using System.Data.Entity;
using itechart.PerformanceReview.Data.Initializers;
using Microsoft.AspNet.Identity.EntityFramework;

namespace itechart.PerformanceReview.Data.Model
{
    public class PerformanceReviewDbContext : IdentityDbContext<User>
    {
        #region DbSets

        public DbSet<Department> Departments { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<EntitiesUpdateHistory> EntitiesUpdateHistories { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }

        #endregion

        public PerformanceReviewDbContext()
            : base("PerformanceReviewDbContext", false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public static PerformanceReviewDbContext Create()
        {
            return new PerformanceReviewDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles", "dbo");

            modelBuilder.Entity<User>()
                .ToTable("Users", "dbo")
                .Ignore(x => x.SessionId)
                .Ignore(x => x.PasswordHash)
                .Ignore(x => x.LockoutEndDateUtc)
                .Ignore(x => x.LockoutEnabled)
                .Ignore(x => x.PhoneNumber)
                .Ignore(x => x.PhoneNumberConfirmed)
                .Ignore(x => x.EmailConfirmed)
                .Ignore(x => x.TwoFactorEnabled);

            modelBuilder.Entity<User>()
                .Property(x => x.UserName)
                .HasColumnName("Login");

            modelBuilder.Entity<User>()
                .HasRequired(x => x.Department)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany<User>(x => x.Users)
                .WithRequired(x => x.Department)
                .HasForeignKey(x => x.DepartmentId);
        }
    }
}
