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

            modelBuilder.Entity<CustomIdentityUser>(user => user.HasIndex(x => x.Locale).IsUnique(false));

            modelBuilder.Entity<Organization>(org =>
            {
                org.ToTable("Organization");
                org.HasKey(x => x.Id);
                org.HasMany<CustomIdentityUser>().WithOne().HasForeignKey(x => x.OrgId).IsRequired(false);
            });
        }
    }
}