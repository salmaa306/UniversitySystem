using Microsoft.EntityFrameworkCore;
using UniversitySystem.Models;


namespace UniversitySystemMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StuCrsRes> StuCrsRes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Avoid multiple cascade paths
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Course)
                .WithMany(c => c.Teachers)
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Composite key for StuCrsRes
            modelBuilder.Entity<StuCrsRes>()
                .HasKey(scr => new { scr.StudentId, scr.CourseId });

            modelBuilder.Entity<StuCrsRes>()
                .HasOne(scr => scr.Student)
                .WithMany(s => s.StuCrsRes)
                .HasForeignKey(scr => scr.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StuCrsRes>()
                .HasOne(scr => scr.Course)
                .WithMany(c => c.StuCrsRes)
                .HasForeignKey(scr => scr.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}