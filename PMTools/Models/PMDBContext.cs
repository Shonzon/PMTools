using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMTools.Models.Project;

namespace PMTools.Models
{
    public class PMDBContext:IdentityDbContext<IdentityUser>
    {
        public PMDBContext(DbContextOptions<PMDBContext> options) : base(options)
        {

        }

        public DbSet<ProjectModel> ProjectTable { get; set; }
        public DbSet<ProjectAssignModel> ProjectAssignTable { get; set; }
        public DbSet<TaskModel> TaskTable { get; set; }
        public DbSet<TaskModelLog> TaskLogTable { get; set; }
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
