using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.CustomIdentityDB.Models
{
    public class CustomIdentityDbContext : IdentityDbContext<CustomIdentityUser>
    {
        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomIdentityUser>(user =>
            {
                user.HasIndex(x => x.Locale).IsUnique(false);
                user.HasMany<UserGroup>().WithOne().HasForeignKey(x => x.UserId).IsRequired(false);
            });

            modelBuilder.Entity<Organization>(org =>
            {
                org.ToTable("Organization");
                org.HasKey(x => x.Id);
                org.HasMany<CustomIdentityUser>().WithOne().HasForeignKey(x => x.OrgId).IsRequired(false);
            });

            modelBuilder.Entity<Group>(grp =>
            {
                grp.ToTable("Groups");
                grp.HasKey(x => x.Id);
                grp.HasMany<Notification>().WithOne().HasForeignKey(x => x.GroupId).IsRequired(true);
                grp.HasMany<UserGroup>().WithOne().HasForeignKey(x => x.GroupId).IsRequired(false);
            });

            modelBuilder.Entity<Notification>(ntf =>
            {
                ntf.ToTable("Notifications");
                ntf.HasKey(x => x.Id);
                ntf.HasOne<Group>();
            });

            modelBuilder.Entity<UserGroup>(ug =>
            {
                ug.ToTable("UserGroups");
                ug.HasKey(x => x.Id);
            });
        }
    }
}