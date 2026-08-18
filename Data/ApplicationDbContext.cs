using Microsoft.EntityFrameworkCore;
using SymphonyPortal.Models;

namespace SymphonyPortal.Data        // Data folder ka namespace
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<EntranceExam> EntranceExams { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Branch> Branches { get; set; }
    }
}