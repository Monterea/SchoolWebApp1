using Microsoft.EntityFrameworkCore;
using SchoolWebApp1.Models;
using System.Security.Cryptography.X509Certificates;

namespace SchoolWebApp1.Data {
    public class ApplicationDbContext:DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
