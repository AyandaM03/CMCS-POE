using CMCS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMCS.Data
{
    public class CMCSContext : DbContext
    {
        public CMCSContext(DbContextOptions<CMCSContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { RoleID = 1, RoleName = "Lecturer" },
                new UserRole { RoleID = 2, RoleName = "Coordinator" },
                new UserRole { RoleID = 3, RoleName = "Manager" },
                new UserRole { RoleID = 4, RoleName = "HR" }
            );
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Document>? Documents { get; set; }      
        public DbSet<Lecturer> Lecturers { get; set; }     
        public DbSet<UserRole>? UserRoles { get; set; }
    }
}



