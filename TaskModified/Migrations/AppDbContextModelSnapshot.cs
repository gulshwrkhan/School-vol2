﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskModified.Data;

#nullable disable

namespace TaskModified.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TaskModified.Model.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseID"), 1L, 1);

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("CourseID");

                    b.HasIndex("CourseCode")
                        .IsUnique();

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("TaskModified.Model.Grade", b =>
                {
                    b.Property<int>("GradeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GradeID"), 1L, 1);

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("GradeValue")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("GradeID");

                    b.HasIndex("CourseID");

                    b.HasIndex("StudentId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("TaskModified.Model.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"), 1L, 1);

                    b.Property<string>("StudenAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentEnrollment")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("StudentGpa")
                        .HasColumnType("real");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("StudentEnrollment")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("TaskModified.Model.StudentCourse", b =>
                {
                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.HasKey("StudentID", "CourseID");

                    b.HasIndex("CourseID");

                    b.ToTable("StudentsCourses");
                });

            modelBuilder.Entity("TaskModified.Model.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"), 1L, 1);

                    b.Property<string>("TeacherAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeacherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeacherPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherSalary")
                        .HasColumnType("int");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("TaskModified.Model.TeacherStudent", b =>
                {
                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<int>("TeacherID")
                        .HasColumnType("int");

                    b.HasKey("StudentID", "TeacherID");

                    b.HasIndex("TeacherID");

                    b.ToTable("TeachersStudents");
                });

            modelBuilder.Entity("TaskModified.Model.Course", b =>
                {
                    b.HasOne("TaskModified.Model.Teacher", "Teachers")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("TaskModified.Model.Grade", b =>
                {
                    b.HasOne("TaskModified.Model.Course", "Courses")
                        .WithMany("Grades")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskModified.Model.Student", "Students")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("TaskModified.Model.StudentCourse", b =>
                {
                    b.HasOne("TaskModified.Model.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskModified.Model.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("TaskModified.Model.TeacherStudent", b =>
                {
                    b.HasOne("TaskModified.Model.Student", "Student")
                        .WithMany("TeacherStudents")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskModified.Model.Teacher", "Teacher")
                        .WithMany("TeacherStudents")
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("TaskModified.Model.Course", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("TaskModified.Model.Student", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("StudentCourses");

                    b.Navigation("TeacherStudents");
                });

            modelBuilder.Entity("TaskModified.Model.Teacher", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("TeacherStudents");
                });
#pragma warning restore 612, 618
        }
    }
}
