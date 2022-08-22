using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyIdentityProvider.Model;
using StudentCourse.Domain;

namespace MyIdentityProvider.DAL
{
    public class AppDbContext : IdentityDbContext<CustomIdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }


    }
}

