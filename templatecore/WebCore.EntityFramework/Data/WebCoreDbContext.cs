using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebCore.Entities;
using WebCore.EntityFramework.Configurations;

namespace WebCore.EntityFramework.Data
{
    public class WebCoreDbContext : IdentityDbContext<WebCoreUser,
        WebCoreRole,
        string,
        WebCoreUserClaim,
        WebCoreUserRole,
        WebCoreUserLogin,
        WebCoreRoleClaim,
        WebCoreUserToken>
    {
        public WebCoreDbContext(DbContextOptions<WebCoreDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WebCoreUser>().ToTable("WebCoreUsers");
            modelBuilder.Entity<WebCoreUserRole>().ToTable("WebCoreUserRoles");
            modelBuilder.Entity<WebCoreUserLogin>().ToTable("WebCoreUserLogins");
            modelBuilder.Entity<WebCoreUserClaim>().ToTable("WebCoreUserClaims");
            modelBuilder.Entity<WebCoreRoleClaim>().ToTable("WebCoreRoleClaims");
            modelBuilder.Entity<WebCoreRole>().ToTable("WebCoreRoles");

            modelBuilder.ApplyConfiguration(new MasterListConfiguration());
        }

        #region Db Register
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageDetail> LanguageDetails { get; set; }
        public DbSet<AppMenu> AppMenus { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }
        public DbSet<MasterList> MasterLists { get; set; }
        public DbSet<MetaDescription> MetaDescriptions { get; set; }
        #endregion
    }
}
