using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using FluentValidation.AspNetCore;
using Listening.Core;
using Listening.Core.Entities.Custom;
using Listening.Core.Profiles;
using Listening.Infrastructure;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Infrastructure.Repositories.Postgres;
using Listening.Infrastructure.Services;
using Listening.Infrastructure.Services.Contracts;
using Listening.Infrastructure.Services.Custom;
using Listening.JobSchedule;
using Listening.JobSchedule.Scheduling;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Entities.Specialized.Text;
using Listening.Server.Repositories.Abstract;
using Listening.Server.Repositories.Duplicates;
using Listening.Server.Repositories.Mongo;
using Listening.Server.Repositories.Postgres;
using Listening.Server.Services;
using Listening.Server.Services.Contracts;
using Listening.Server.Services.Duplicates;
using Listening.Server.Utilities;
using Listening.Web.Filters;
using Listening.Web.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using OpenIddict.Abstractions;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices.Extensions;
using Listening.Core.ViewModels.ListeningResult;

namespace Listening.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomizedMvc(this IServiceCollection services, string allowSpecificOrigins)
        {
            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(ModelValidationFilter));
                    //options.Filters.Add(new CorsAuthorizationFilterFactory(allowSpecificOrigins));
                })
                //.AddJsonOptions(options =>
                //{
                //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //})
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddHttpContextAccessor();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        public static IServiceCollection AddCustomizedHSTS(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                //options.ExcludedHosts.Add("example.com");
                //options.ExcludedHosts.Add("www.example.com");
            });

            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    // options for user and password can be set here
                    // options.Password.RequiredLength = 4;
                    // options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddCustomOpenIddict(this IServiceCollection services, IWebHostEnvironment env)
        {
            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            // Register the OpenIddict services.
            //services.AddOpenIddict(options =>
            //{
            //    // Register the Entity Framework stores.
            //    //options.AddEntityFrameworkCoreStores<ApplicationDbContext>();
            //    options.AddCore(opts =>
            //    {
            //        opts.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>();
            //    });

            //    // Register the ASP.NET Core MVC binder used by OpenIddict.
            //    // Note: if you don't call this method, you won't be able to
            //    // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
            //    options.AddMvcBinders();
            //    //options.UseMvc

            //    // Enable the token endpoint.
            //    // Form password flow (used in username/password login requests)
            //    options.EnableTokenEndpoint("/connect/token");

            //    // Enable the authorization endpoint.
            //    // Form implicit flow (used in social login redirects)
            //    options.EnableAuthorizationEndpoint("/connect/authorize");

            //    // Enable the password and the refresh token flows.
            //    options.AllowPasswordFlow()
            //           .AllowRefreshTokenFlow()
            //           .AllowImplicitFlow(); // To enable external logins to authenticate

            //    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(120));
            //    options.SetIdentityTokenLifetime(TimeSpan.FromMinutes(120));
            //    options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(200));

            //    // During development, you can disable the HTTPS requirement.
            //    if (env.IsDevelopment())
            //    {
            //        options.DisableHttpsRequirement();
            //    }

            //    // Note: to use JWT access tokens instead of the default
            //    // encrypted format, the following lines are required:
            //    //
            //    // options.UseJsonWebTokens();
            //    options.AddEphemeralSigningKey();
            //});

            services.AddOpenIddict()
                .AddCore(opts =>
                {
                    opts.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>();
                })
                .AddServer(options =>
                {
                    // Register the ASP.NET Core MVC binder used by OpenIddict.
                    // Note: if you don't call this method, you won't be able to
                    // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                    options.UseMvc();

                    // Enable the token endpoint (required to use the password flow).
                    //options.EnableTokenEndpoint("/connect/token");

                    //options.EnableAuthorizationEndpoint("/connect/authorize");
                    options.EnableAuthorizationEndpoint("/connect/authorize")
                        .EnableTokenEndpoint("/connect/token");

                    // Allow client applications to use the grant_type=password flow.
                    options.AllowPasswordFlow()
                        .AllowRefreshTokenFlow()
                        .AllowImplicitFlow();

                    options.RegisterScopes(OpenIdConnectConstants.Scopes.Email,
                               OpenIdConnectConstants.Scopes.Profile,
                               OpenIddictConstants.Scopes.Roles);

                    options.SetAuthorizationCodeLifetime(TimeSpan.FromMinutes(180));
                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(180));
                    options.SetIdentityTokenLifetime(TimeSpan.FromMinutes(180));
                    options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(180));

                    // During development, you can disable the HTTPS requirement.
                    if (env.IsDevelopment())
                        options.DisableHttpsRequirement();

                    // Accept token requests that don't specify a client_id.
                    options.AcceptAnonymousClients();
                    //options.DisableScopeValidation();

                    options.AddEphemeralSigningKey();

                    options.DisableHttpsRequirement();
                });

            // If you prefer using JWT, don't forget to disable the automatic
            // JWT -> WS-Federation claims mapping used by the JWT middleware:
            //
            // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            // JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            //
            // services.AddAuthentication()
            //     .AddJwtBearer(options =>
            //     {
            //         options.Authority = "http://localhost:54895/";
            //         options.Audience = "resource_server";
            //         options.RequireHttpsMetadata = false;
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             NameClaimType = OpenIdConnectConstants.Claims.Subject,
            //             RoleClaimType = OpenIdConnectConstants.Claims.Role
            //         };
            //     });

            // Alternatively, you can also use the introspection middleware.
            // Using it is recommended if your resource server is in a
            // different application/separated from the authorization server.
            //
            // services.AddAuthentication()
            //     .AddOAuthIntrospection(options =>
            //     {
            //         options.Authority = new Uri("http://localhost:54895/");
            //         options.Audiences.Add("resource_server");
            //         options.ClientId = "resource_server";
            //         options.ClientSecret = "875sqd4s5d748z78z7ds1ff8zz8814ff88ed8ea4z4zzd";
            //         options.RequireHttpsMetadata = false;
            //     });

            services.AddAuthentication(options =>
                {
                    // This will override default cookies authentication scheme
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
               .AddOAuthValidation()
               // https://console.developers.google.com/projectselector/apis/library?pli=1
               .AddGoogle(options =>
               {
                   options.ClientId = Startup.Configuration["Authentication:Google:ClientId"];
                   options.ClientSecret = Startup.Configuration["Authentication:Google:ClientSecret"];
               })
               // https://developers.facebook.com/apps
               .AddFacebook(options =>
               {
                   options.AppId = Startup.Configuration["Authentication:Facebook:AppId"];
                   options.AppSecret = Startup.Configuration["Authentication:Facebook:AppSecret"];
               })
               // https://apps.twitter.com/
               .AddTwitter(options =>
               {
                   options.ConsumerKey = Startup.Configuration["Authentication:Twitter:ConsumerKey"];
                   options.ConsumerSecret = Startup.Configuration["Authentication:Twitter:ConsumerSecret"];
               });
            // https://apps.dev.microsoft.com/?mkt=en-us#/appList
            //.AddMicrosoftAccount(options =>
            //{
            //    options.ClientId = Startup.Configuration["Authentication:Microsoft:ClientId"];
            //    options.ClientSecret = Startup.Configuration["Authentication:Microsoft:ClientSecret"];
            //});

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Add framework services.
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                var connection = configuration["Data:SqlPostegresConnectionString"];
                options.UseNpgsql(connection);
                options.UseNpgsql(connection, b => b.MigrationsAssembly("Listening.Web"));
                options.UseOpenIddict();
            });
            return services;
        }

        public static IServiceCollection AddCustomLocalization(this IServiceCollection services, IWebHostEnvironment hostingEnvironment)
        {
            var translationFile = hostingEnvironment.GetTranslationFile();

            var cultures = translationFile.First().Split(",").Skip(1);

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = cultures.Select(c => new CultureInfo(c)).ToList();

                opts.DefaultRequestCulture = new RequestCulture(cultures.First());
                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            return services;
        }

        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {
            // New instance every time, only configuration class needs so its ok
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddSingleton<IStringLocalizerFactory, EFStringLocalizerFactory>();
            services.AddTransient<IApplicationDataService, ApplicationDataService>();
            services.AddScoped<IUnitOfWork, HttpUnitOfWork>();
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<UserResolverService>();
            services.AddScoped<ApiExceptionFilter>();
            return services;
        }

        public static IServiceCollection RegisterAdditionalServices(this IServiceCollection services)
        {
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            
            services.AddScoped<ILogService, LogService>();

            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatService, ChatService>();

            services.AddScoped<IOpticalCharacterRecognitionService, OpticalCharacterRecognitionService>();

            services.AddScoped<IBackupService, BackupService>();

            services.AddScoped<IYouTubeDownloaderService, YouTubeDownloaderService>();

            services.AddSingleton<IWebPageService, WebPageService>();
            services.AddSingleton<IDebianFAIService, DebianFAIService>();
            services.AddSingleton<IFunctionService, FunctionService>();

            services.AddScoped<ISpecCourseEFRepository, SpecCourseEFRepository>();
            services.AddScoped<ISpecService, SpecService>();

            services.AddSingleton<IStegDataOperationsService, StegDataOperationsService>();
            services.AddScoped<IStegPictureService, StegPictureService>();

            return services;
        }

        public static IServiceCollection RegisterListeningTextServices(this IServiceCollection services)
        {
            //services.AddSingleton(Mapper.Instance);
            services.AddSingleton<UserConnections>();
            services.AddSingleton<IGlobalCache<TextEnhanced, string>, GlobalCache<TextEnhanced, string>>();

            // TODO: uncomment if really need
            // services.AddSingleton<IGlobalCache<Result, long>, GlobalCache<Result, long>>();
            services.AddSingleton<ISimpleTimeSpentCache<ResultIdDto>, SimpleTimeSpentCache<ResultIdDto>>();
            //services.AddScoped<IRepositoryMongo<Text, ObjectId>, TextsMongoRepository>();
            services.AddScoped<ITextsMongoRepository, TextsMongoRepository>();
            services.AddScoped<IErrorsForSeparatedRepository, ErrorForSeparatedRepository>();
            services.AddScoped<IErrorsForJoinedRepository, ErrorForJoinedRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddTransient<IResultEFRepository, ResultEFRepository>();

            services.AddScoped<ITextService, TextService>();
            services.AddScoped<IResultService, ResultService>();
            services.AddScoped<IFileService, FileService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            //services.AddScoped<IUniqueAppIdGeneratorRepository, UniqueAppIdGeneratorRepository>();
            return services;
        }

        public static IServiceCollection RegisterBlogServices(this IServiceCollection services)
        {
            services.AddScoped<IPostEFRepository, PostEFRepository>();
            services.AddScoped<IBlogService, BlogService>();

            return services;
        }

        public static IServiceCollection RegisterCORS(this IServiceCollection services, string allowSpecificOrigins, IWebHostEnvironment env, IConfiguration config)
        {
            if (env.IsDevelopment())
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(allowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5000",
                            "https://localhost:5001",
                            "http://localhost:8443",
                            "https://localhost:8443")
                        .AllowAnyMethod();
                    });
                });
            }
            else
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(allowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins($"http://{config["DomainName"]}",
                            $"https://{config["DomainName"]}",
                            $"http://{config["DomainName"]}:8443",
                            $"https://{config["DomainName"]}:8443")
                        .AllowAnyMethod();
                    });
                });
            }


            return services;
        }

        public static IServiceCollection CreateFolders(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        {
            var logPath = $"{env.ContentRootPath}{configuration["Data:FileStorage:Logs"]}";

            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            return services;
        }

        public static IServiceCollection BuildAppId(this IServiceCollection services)
        {
            Task.Run(() =>
            {
                Task.Delay(30000).Wait();
                new UniqueAppIdGeneratorRepository(Startup.Configuration).BuildAppId();
            });

            return services;
        }

        public static IServiceCollection AddJobSchedule(this IServiceCollection services)
        {
            services.AddSingleton<ITextMongoRepoDuplicate, TextMongoRepoDuplicate>();
            services.AddSingleton<IFileServiceDuplicate, FileServiceDuplicate>();
            services.AddSingleton<ICleanupService, CleanupService>();
            services.AddSingleton<IScheduledTask, CleanUpTask>();

            // Add scheduled tasks & scheduler
            services.AddScheduler((sender, args) =>
            {
                var service = Startup.ServiceProvider.GetService<ILoggerFactory>();
                var logger = service.CreateLogger("JobSchedule");
                logger.LogError(args.Exception.Message);
                logger.LogError(args.Exception.StackTrace);
                args.SetObserved();
            });

            return services;
        }

        public static IServiceCollection RegisterMapping(this IServiceCollection services)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AdminProfile>();
                cfg.AddProfile<AccountProfile>();
                cfg.AddProfile<ListeningProfile>();
                cfg.AddProfile<ResultProfile>();
                cfg.AddProfile<ChatMapperProfile>();
                cfg.AddProfile<FeedbackProfile>();
                cfg.AddProfile<BlogProfile>();
                cfg.AddProfile<SpecProfile>();
            }));

            services.AddSingleton<IMapper>(mapper);

            return services;
        }
    }
}
