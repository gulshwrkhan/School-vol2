using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModified.Model;

namespace TaskModified.Data
{
    public class AppDbContext:DbContext
    {
        public string ConnectionString { get; }
        public DbSet<Course>Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set;}
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherStudent> TeachersStudents { get;set; }
        public AppDbContext()
        {
            ConnectionString = "Data Source=DESKTOP-A2HB7G2;Initial Catalog=data;Integrated Security=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluentApi for many to many relationship of Student and Course
            modelBuilder.Entity<StudentCourse>()
                .HasKey(e => new { e.StudentID, e.CourseID });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(e => e.Student)
                .WithMany(e => e.StudentCourses)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(e => e.Course)
                .WithMany(e => e.StudentCourses)
                .HasForeignKey(e => e.CourseID);

            //fluentApi for many to many relationship of Teacher and Students
            modelBuilder.Entity<TeacherStudent>()
                .HasKey(e => new { e.StudentID, e.TeacherID });

            modelBuilder.Entity<TeacherStudent>()
                .HasOne(e => e.Student)
                .WithMany(e => e.TeacherStudents)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<TeacherStudent>()
                .HasOne(e => e.Teacher)
                .WithMany(e => e.TeacherStudents)
                .HasForeignKey(e => e.TeacherID);

            //fluentApi for uniqueness for enrollment of studnet
            modelBuilder.Entity<Student>()
                .HasIndex(s=>s.StudentEnrollment)
                .IsUnique();

            //fluentApi for uniqueness of courcecode of course
            modelBuilder.Entity<Course>()
                .HasIndex(e => e.CourseCode)
                .IsUnique();

        }
    }
}
