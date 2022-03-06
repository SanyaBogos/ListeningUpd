
using Listening.Core.Entities;
using Listening.Core.Entities.Custom;
using Listening.Core.Entities.Specialized;
using Listening.Core.Entities.Specialized.Chat;
using Listening.Core.Entities.Specialized.Knowledge;
using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.Entities.Specialized.MiniBlog;
using Listening.Infrastructure.Services;
using Listening.Infrastructure.Services.Custom;
using Listening.Server.Entities.Specialized.Result;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Listening.Core.Entities.Specialized.Crossword;

namespace Listening.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        private readonly UserResolverService _userService;

        public string CurrentUserId { get; internal set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserPhoto> ApplicationUserPhotos { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Resource> Resources { get; set; }

        public DbSet<Result> Results { get; set; }
        public DbSet<ErrorForSeparated> ErrorsForSeparated { get; set; }
        public DbSet<ErrorForJoined> ErrorsForJoined { get; set; }

        public DbSet<Dialogue> Dialogues { get; set; }
        public DbSet<ChatGroup> ChatGroups { get; set; }
        public DbSet<ChatUsersInGroups> ChatUsersInGroups { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<UniqueAppIdGenerator> UniqueAppIdGenerator { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTopic> PostTopics { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Topic> Topics { get; set; }

        //public DbSet<Core.Entities.Specialized.TimeCode.Video> Videos { get; set; }
        //public DbSet<TimeStamp> TimeStamps { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Core.Entities.Specialized.Knowledge.Type> Types { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<VideoType> VideoTypes { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Access> Accesses { get; set; }

        public DbSet<Crossword> Crosswords { get; set; }
        public DbSet<CrosswordDescription> CrosswordDescriptions { get; set; }
        public DbSet<CrosswordResult> CrosswordResults { get; set; }
        public DbSet<QuestionDescription> QuestionDescriptions { get; set; }
        public DbSet<WordDescription> WordDescriptions { get; set; }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            UserResolverService userService)
            : base(options)
        {
            _userService = userService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                    .Where(e => typeof(IAuditable).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("CreatedAt");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("UpdatedAt");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("CreatedBy");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("UpdatedBy");
            }

            modelBuilder.Entity<ChatUsersInGroups>()
                .HasKey(c => new { c.UserId, c.ChatGroupId });

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(c => new { c.SignalRId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        /// <summary>
        /// Override SaveChanges so we can call the new AuditEntities method.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            this.AuditEntities();
            return base.SaveChanges();
        }
        /// <summary>
        /// Override SaveChangesAsync so we can call the new AuditEntities method.
        /// </summary>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.AuditEntities();

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Method that will set the Audit properties for every added or modified Entity marked with the 
        /// IAuditable interface.
        /// </summary>
        private void AuditEntities()
        {

            DateTime now = DateTime.Now;
            // Get the authenticated user name 
            string userName = _userService.GetUser();

            // For every changed entity marked as IAditable set the values for the audit properties
            foreach (EntityEntry<IAuditable> entry in ChangeTracker.Entries<IAuditable>())
            {
                // If the entity was added.
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedBy").CurrentValue = userName;
                    entry.Property("CreatedAt").CurrentValue = now;
                }
                else if (entry.State == EntityState.Modified) // If the entity was updated
                {
                    entry.Property("UpdatedBy").CurrentValue = userName;
                    entry.Property("UpdatedAt").CurrentValue = now;
                }
            }
        }
    }
}
