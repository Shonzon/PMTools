using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PMTools.Models
{
    public class PMDBContext:IdentityDbContext<IdentityUser>
    {
        public PMDBContext(DbContextOptions<PMDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            AssignRoles(builder);
        }

        private void AssignRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                    new IdentityRole { Name = "Developer", ConcurrencyStamp = "2", NormalizedName = "Developer" }
                );
        }
    }
}
