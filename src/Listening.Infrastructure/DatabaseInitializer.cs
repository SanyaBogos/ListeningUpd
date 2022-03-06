using Listening.Core;
using Listening.Core.Entities.Custom;
using Listening.Core.Entities.Specialized.Knowledge;
using Listening.Core.Entities.Specialized.MiniBlog;
using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Infrastructure.Seeds.Courses;
using Listening.Infrastructure.Seeds.DevelopData;
using Listening.Infrastructure.Seeds.Folders;
using Listening.Server;
using Listening.Server.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeType = Listening.Core.Entities.Specialized.Knowledge.Type;

namespace Listening.Infrastructure
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync(IConfiguration configuration);
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _openIddictApplicationManager;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IPostEFRepository _postRepository;

        public DatabaseInitializer(
            ApplicationDbContext context,
            ILogger<DatabaseInitializer> logger,
            OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment hostingEnvironment,
            IPostEFRepository postRepository
            )
        {
            _context = context;
            _logger = logger;
            _openIddictApplicationManager = openIddictApplicationManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            _postRepository = postRepository;
        }

        public async Task SeedAsync(IConfiguration configuration)
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);

            await CreateRoles().ConfigureAwait(false);
            await CreateUsers().ConfigureAwait(false);

            await CreateTopicsForBlog().ConfigureAwait(false);
            await CreateMediaTypesForBlog().ConfigureAwait(false);
            await CreatePrioritiesForBlog().ConfigureAwait(false);

            // temporary cleanup - 
            // TODO: SHOULD BE COMMENTED AFTER RELEASE
            // await _context.Database.ExecuteSqlRawAsync($@"TRUNCATE public.""Knowledge_Access"", public.""Knowledge_Authors"",  
            //         public.""Knowledge_Books"", public.""Knowledge_Folders"", 
			//         public.""Knowledge_TimeStamps"", public.""Knowledge_Videos"", public.""Knowledge_FileTypes"",
			//         public.""Knowledge_VideoTypes"", public.""Knowledge_Types"",
			//         public.""Knowledge_Courses"" RESTART IDENTITY;");
            // await _context.SaveChangesAsync();
            //----------------------------------------------

            await CreateTypesForKnowledge().ConfigureAwait(false);
            await CreateAuthorsForKnowledge().ConfigureAwait(false);
            await CreateFileTypesForKnowledge().ConfigureAwait(false);
            await CreateVideoTypesForKnowledge().ConfigureAwait(false);
            await CreateCoursesForKnowledge().ConfigureAwait(false);
            await CreateFoldersForKnowledge().ConfigureAwait(false);
            await CreateAccessForKnowledge().ConfigureAwait(false);

            await AddLocalisedData().ConfigureAwait(false);
            await AddOpenIdConnectOptions(configuration).ConfigureAwait(false);
        }

        private async Task CreateAccessForKnowledge()
        {
            if (_context.Accesses.Any())
                return;

            var spec = _context.Roles.First(x => x.Name == "Specific");
            var specAdmin = _context.Roles.First(x => x.Name == "SpecAdm");

            var coursesAll = _context.Courses.Where(x => x.Name != "Проектирование родовго поместья"
                                                                    && x.Name != "Хранители и берегини семьи"
                                                                    && x.Name != "Консультант по предназначению");
            var coursesAdm = _context.Courses.Where(x => x.Name == "Проектирование родовго поместья"
                                                                    || x.Name == "Хранители и берегини семьи"
                                                                    || x.Name == "Консультант по предназначению");
            var accessSpec = new List<Access>();

            foreach (var course in coursesAll)
            {
                accessSpec.Add(new Access { RoleId = spec.Id, CourseId = course.Id });
                accessSpec.Add(new Access { RoleId = specAdmin.Id, CourseId = course.Id });
            }

            foreach (var course in coursesAdm)
                accessSpec.Add(new Access { RoleId = specAdmin.Id, CourseId = course.Id });

            _context.Accesses.AddRange(accessSpec);
            await _context.SaveChangesAsync();
        }

        private async Task CreateFoldersForKnowledge()
        {
            if (_context.Folders.Any())
                return;

            var videoType = _context.VideoTypes.First(x => x.Name == "mp4");
            var folderId = 1;
            var videoId = 1;

            var peretyatCourse = _context.Courses.First(x => x.Name == "Ореховый лесосад");
            var peretyatFolders = PeretNatGuardFolders.GetFolders(folderId, videoId, videoType.Id, peretyatCourse.Id);
            folderId += peretyatFolders.Length;
            videoId += peretyatFolders.Sum(x => x.Videos.Count);


            var shirCourse = _context.Courses.First(x => x.Name == "Проектируем себе экодом");
            var shirFolders = ShirEcobuildFolders.GetFolders(folderId, videoId, videoType.Id, shirCourse.Id);
            folderId += shirFolders.Length;
            videoId += shirFolders.Sum(x => x.Videos.Count);


            var kovCourse = _context.Courses.First(x => x.Name == "Солэкодом");
            var kovFolders = KovalEcobuildFolders.GetFolders(folderId, videoId, videoType.Id, kovCourse.Id);
            folderId += kovFolders.Length;
            videoId += kovFolders.Sum(x => x.Videos.Count);


            var orlovCourse = _context.Courses.First(x => x.Name == "Проектирование родовго поместья");
            var orlovFolders = OrlovNatGuardFolders.GetFolders(folderId, videoId, videoType.Id, orlovCourse.Id);
            folderId += orlovFolders.Length;
            videoId += orlovFolders.Sum(x => x.Videos.Count);


            var frolov = _context.Authors.First(x => x.Name == "frolov");
            var miniInfoProductName = "Мини инфопродукт";
            var frolovCourseIds = _context.Courses.Where(x => x.Author.Id == frolov.Id && !x.Name.Contains(miniInfoProductName))
                .ToDictionary(x => x.Name.GetNumberAfterSymbol("№", 2), x => x.Id);

            var frolovMiniCourseIds = _context.Courses.Where(x => x.Author.Id == frolov.Id && x.Name.Contains(miniInfoProductName))
                .ToDictionary(x => x.Name.GetNumberAfterSymbol("№", 2), x => x.Id);

            var frolovFolders = FrolovFolders.GetFolders(folderId, videoId,
                        _context.VideoTypes.OrderBy(x => x.Id).Select(x => x.Id).ToArray(),
                        frolovCourseIds, frolovMiniCourseIds);

            _context.Folders.AddRange(peretyatFolders);
            _context.Folders.AddRange(shirFolders);
            _context.Folders.AddRange(kovFolders);
            _context.Folders.AddRange(orlovFolders);
            _context.Folders.AddRange(frolovFolders);
            await _context.SaveChangesAsync();
        }

        private async Task CreateCoursesForKnowledge()
        {
            if (_context.Courses.Any())
                return;

            var ecobuildType = _context.Types.First(x => x.Name == "ecobuild");

            var shirok = _context.Authors.First(x => x.Name == "shirokov");
            var koval = _context.Authors.First(x => x.Name == "kovalenko");
            var kedr = _context.Authors.First(x => x.Name == "kedr");

            var pdf = _context.FileTypes.First(x => x.Name == "pdf");
            var djvu = _context.FileTypes.First(x => x.Name == "djvu");

            var id = 1;
            var bookId = 1;

            var ecobuildCourses = EcobuildCourses.GetCourses(id, bookId, ecobuildType.Id,
                new int[] { shirok.Id, koval.Id, kedr.Id },
                new int[] { pdf.Id, djvu.Id });

            id += ecobuildCourses.Length;
            bookId += ecobuildCourses.Sum(x => x.Books.Count);


            var natureGuardenType = _context.Types.First(x => x.Name == "nature_gard");
            var peretyat = _context.Authors.First(x => x.Name == "peretyat");
            var orlov = _context.Authors.First(x => x.Name == "orlov");

            var natureGuardenCourses = NatureGuardenFolders.GetCourses(id, natureGuardenType.Id, new int[] { peretyat.Id, orlov.Id });
            id += natureGuardenCourses.Length;


            var healthType = _context.Types.First(x => x.Name == "health");
            var sovetov = _context.Authors.First(x => x.Name == "sovetov");

            var healthCourses = SovetovCourses.GetCourses(id, healthType.Id, sovetov.Id);
            id += healthCourses.Length;


            var healType = _context.Types.First(x => x.Name == "energo_heal");
            var milaart = _context.Authors.First(x => x.Name == "milaart");

            var healCourses = MilaCourses.GetCourses(id, healType.Id, milaart.Id);
            id += healCourses.Length;


            var survive = _context.Types.First(x => x.Name == "survv");
            var tuning = _context.Types.First(x => x.Name == "tun");
            var frolov = _context.Authors.First(x => x.Name == "frolov");
            var frolovCourses = FrolovCourses.GetCourses(id, frolov.Id,
                new int[] { healthType.Id, natureGuardenType.Id, ecobuildType.Id, survive.Id, tuning.Id });

            _context.Courses.AddRange(ecobuildCourses);
            _context.Courses.AddRange(natureGuardenCourses);
            _context.Courses.AddRange(healthCourses);
            _context.Courses.AddRange(healCourses);
            _context.Courses.AddRange(frolovCourses);
            await _context.SaveChangesAsync();
        }

        private async Task CreateTypesForKnowledge()
        {
            if (_context.Types.Any())
                return;

            var id = 1;

            var knowledgeTypesStrings = new string[] { "ecobuild", "nature_gard", "health", "energo_heal", "survv", "tun" };
            var types = knowledgeTypesStrings.Select(x => new KnowledgeType { Id = id++, Name = x }).ToArray();

            _context.Types.AddRange(types);
            await _context.SaveChangesAsync();
        }

        private async Task CreateAuthorsForKnowledge()
        {
            if (_context.Authors.Any())
                return;

            var id = 1;

            var authorsStrings = new string[] { "shirokov", "kovalenko", "kedr", "peretyat", "orlov", "sovetov", "milaart", "frolov" };
            var authors = authorsStrings.Select(x => new Author { Id = id++, Name = x }).ToArray();

            _context.Authors.AddRange(authors);
            await _context.SaveChangesAsync();
        }

        private async Task CreateFileTypesForKnowledge()
        {
            if (_context.FileTypes.Any())
                return;

            var id = 1;

            var fileTypesStrings = new string[] { "pdf", "djvu", "txt", "doc", "docx", "xls", "xlsx", "csv" };
            var fileTypes = fileTypesStrings.Select(x => new FileType { Id = id++, Name = x }).ToArray();

            _context.FileTypes.AddRange(fileTypes);
            await _context.SaveChangesAsync();
        }

        private async Task CreateVideoTypesForKnowledge()
        {
            if (_context.VideoTypes.Any())
                return;

            var id = 1;

            var videoTypeStrings = new string[] { "mp4", "webm", "avi", "mkv", "flv", "vob", "ogv", "ogg",
                "mov", "wmv", "amv", "m4p", "m4v", "mpg", "mp2", "mpeg" };

            var videoType = videoTypeStrings.Select(x => new VideoType { Id = id++, Name = x }).ToArray();

            _context.VideoTypes.AddRange(videoType);
            await _context.SaveChangesAsync();
        }

        private async Task CreatePrioritiesForBlog()
        {
            if (_context.Priorities.Any())
                return;

            var id = 1;

            var prioritiesStrings = new string[] { "methodology", "chronology", "factology", "economy",
                "genocide", "war", "uncategorized" };
            var priorities = prioritiesStrings.Select(x => new Priority { Id = id++, Name = x }).ToArray();

            _context.Priorities.AddRange(priorities);
            await _context.SaveChangesAsync();
        }

        private async Task CreateMediaTypesForBlog()
        {
            if (_context.MediaTypes.Any())
                return;

            var id = 1;

            var mediaTypesStrings = new string[] { "audio", "video", "file", "application" };
            var mediaTypes = mediaTypesStrings.Select(x => new MediaType { Id = id++, Name = x }).ToArray();

            _context.MediaTypes.AddRange(mediaTypes);
            await _context.SaveChangesAsync();
        }

        private async Task CreateTopicsForBlog()
        {
            if (_context.Topics.Any())
                return;

            var topics = new Topic[]
            {
                new Topic { Name = "nature" },
                new Topic { Name = "world" },
                new Topic { Name = "trees" },
                new Topic { Name = "worldview" },
            };

            _context.Topics.AddRange(topics);
            await _context.SaveChangesAsync();
        }

        private async Task CreateRoles()
        {
            var rolesToAdd = new List<ApplicationRole>()
            {
                new ApplicationRole { Name = GlobalConstats.USER, Description = "Limited rights role"},
                new ApplicationRole { Name = GlobalConstats.MODERATOR, Description = "Enhanced rights role"},
                new ApplicationRole { Name = GlobalConstats.ADMIN, Description = "Full rights role"},
                new ApplicationRole { Name = GlobalConstats.SUPER, Description = "Extremum rights role"},
                new ApplicationRole { Name = GlobalConstats.SPECIFIC, Description = "Specific rights role"},
                new ApplicationRole { Name = GlobalConstats.SPECADM, Description = "Specific admin rights role"},
            };

            var diff = rolesToAdd.Select(x => x.Name).Except(_context.Roles.Select(x => x.Name)).ToArray();

            if (!diff.Any())
                return;

            var toAdd = rolesToAdd.Where(x => diff.Contains(x.Name)).ToArray();
            foreach (var role in toAdd)
                if (!await _roleManager.RoleExistsAsync(role.Name))
                    await _roleManager.CreateAsync(role);
        }

        private async Task CreateUsers()
        {
            var fromFile = SecurityRulesSingleton.Instance.Rules.Users.Select(x => x.Email);
            var toAddEmails = fromFile.Except(_context.ApplicationUsers.Select(x => x.Email)).ToArray();

            if (!toAddEmails.Any())
                return;

            await DevelopData();

            var toAdd = SecurityRulesSingleton.Instance.Rules.Users.Where(x => toAddEmails.Contains(x.Email)).ToArray();

            foreach (var user in toAdd)
            {
                var appUser = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    IsEnabled = true
                };

                await CreateUser(appUser, user.Role, user.Password);
            }
        }

        private async Task AddLocalisedData()
        {
            // clear tables and change identity to 0
            await _context.Database.ExecuteSqlRawAsync($"TRUNCATE public.\"Translate_Resources\", public.\"Translate_Cultures\" RESTART IDENTITY;");
            await _context.SaveChangesAsync();

            var translations = _hostingEnvironment.GetTranslationFile();
            var locales = translations.First().Split(",").Skip(1).ToList();
            var currentLocale = 0;

            locales.ForEach(locale =>
            {
                currentLocale++;

                var culture = new Culture { Name = locale };
                var resources = new List<Resource>();

                translations.Skip(1).ToList().ForEach(t =>
                {
                    var line = t.Split(",");
                    resources.Add(new Resource
                    {
                        Culture = culture,
                        Key = line[0],
                        Value = line[currentLocale].Replace("%comma%", ",")
                    });
                });

                culture.Resources = resources;
                _context.Cultures.Add(culture);
            });

            await _context.SaveChangesAsync();
        }

        private async Task AddOpenIdConnectOptions(IConfiguration configuration)
        {
            if (await _openIddictApplicationManager.FindByClientIdAsync("listening") == null)
            {
                var host = configuration["HostUrl"].ToString();

                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "listening",
                    DisplayName = "Listening",
                    PostLogoutRedirectUris = { new Uri($"{host}signout-oidc") },
                    RedirectUris = { new Uri(host) },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.Implicit,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken
                    }
                };

                await _openIddictApplicationManager.CreateAsync(descriptor);
            }
        }

        private async Task DevelopData()
        {
            if (!_hostingEnvironment.IsDevelopment())
                return;

            var pwd = "P@ssw0rd!";

            var usersWithRoles = UsersInsert.GetUsersWithRoles();

            foreach (var user in usersWithRoles.Keys)
            {
                await _userManager.CreateAsync(user, pwd);
                await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user.Email), usersWithRoles[user]);
            }
        }

        private async Task CreateUser(ApplicationUser user, string role, string password)
        {
            var existingAdminUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingAdminUser != null)
            {
                if (!(await _userManager.IsInRoleAsync(existingAdminUser, role)))
                    await _userManager.AddToRoleAsync(existingAdminUser, role);
            }
            else
            {
                await _userManager.CreateAsync(user, password);
                await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
