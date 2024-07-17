using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskModified.Data;
using TaskModified.Interface;
using TaskModified.Model;

namespace TaskModified.Services
{
    public class StudentService:IStudent
    {
        public void AddStudent(Student Students) 
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var exist = context.Students.Any(e => e.StudentEnrollment == Students.StudentEnrollment);
                    if(!exist)
                    {
                        context.Students.Add(Students);
                        context.SaveChanges();
                        Console.WriteLine("Student added successfully.\n Press Enter to continue...");
                    }
                    else
                    {
                        Console.WriteLine("Enrollment is used before,Thorefore student not added......");
                    }
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the student: {ex.Message}");
            }
        }
        public void RemoveStudent(string StudentEnrollment) 
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var man = context.Students.Single(e => e.StudentEnrollment == StudentEnrollment);
                    context.Students.Remove(man);
                    context.SaveChanges();
                }
                Console.WriteLine("Student Removed successfully.\n Press Enter to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing the student: {ex.Message}");
            }

        }
        public void UpdateStudent(Student std, string StudentEnrollment) 
        {
            try
            {
                using(var context = new AppDbContext())
                {
                    var st=context.Students.Single(e=>e.StudentEnrollment==StudentEnrollment);
                    st.StudentName = std.StudentName;
                    st.StudentEnrollment = std.StudentEnrollment;
                    st.StudentPhone = std.StudentPhone;
                    st.StudenAddress= std.StudenAddress;
                    st.StudentGpa = std.StudentGpa;
                    context.SaveChanges();
                }
                Console.WriteLine("student Updated Successfully.\n Press Enter to continue...");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the student: {ex.Message}");
            }

        }
        public void ListAllStudents()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Listing All Students\n\n");

                using (var context = new AppDbContext())
                {
                    var assignments = (from student in context.Students
                                       join studCourse in context.StudentsCourses
                                       on student.StudentId equals studCourse.StudentID into studentCources
                                       from studCourse in studentCources.DefaultIfEmpty()
                                       join grade in context.Grades.AsNoTracking() 
                                       on new { student.StudentId, studCourse.CourseID } equals new { grade.StudentId, grade.CourseID } into studentGrades
                                       from grade in studentGrades.DefaultIfEmpty()
                                       join teacher in context.TeachersStudents.AsNoTracking() 
                                       on student.StudentId equals teacher.StudentID into studentTeacher
                                       from teacher in studentTeacher.DefaultIfEmpty()
                                       group new { student, studCourse.Course, grade, teacher.Teacher } by new
                                       {
                                           student.StudentId,
                                           student.StudentEnrollment,
                                           student.StudentPhone,
                                           student.StudentGpa,
                                           student.StudentName,
                                           student.StudenAddress
                                       } into studentGroup
                                       select new
                                       {
                                           StudentId = studentGroup.Key.StudentId,
                                           StudentEnrollment = studentGroup.Key.StudentEnrollment,
                                           StudentPhone = studentGroup.Key.StudentPhone,
                                           StudentGpa = studentGroup.Key.StudentGpa,
                                           StudentName = studentGroup.Key.StudentName,
                                           StudenAddress = studentGroup.Key.StudenAddress,
                                           Courses = studentGroup.Select(g => g.Course.CourseName).Distinct().ToList(),
                                           Grades = studentGroup.Select(g => g.grade.GradeValue).Distinct().ToList(),
                                           Teachers = studentGroup.Select(g => g.Teacher.TeacherName).Distinct().ToList()
                                       }).ToList();

                    foreach (var student in assignments)
                    {
                        Console.WriteLine($"Student {student.StudentId}");
                        Console.WriteLine($"\tStudent's Enrollment: {student.StudentEnrollment}");
                        Console.WriteLine($"\tStudent's Name: {student.StudentName}");
                        Console.WriteLine($"\tStudent's Gpa: {student.StudentGpa}");
                        Console.WriteLine($"\tStudent's Address: {student.StudenAddress}");
                        Console.WriteLine($"\tStudent's PhoneNumber: {student.StudentPhone}");
                        if(student.Courses.Count!=0)
                        {
                            Console.WriteLine("\tStudent's Courses and Grades:");
                            for (int i = 0; i < student.Courses.Count; i++)
                            {
                                var grade = i < student.Grades.Count ? student.Grades[i] : "(no grade assigned)";
                                Console.WriteLine($"\t\tCourse: {student.Courses[i]} \tGrade: {grade}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\tNo cources registered..");
                        }
                        

                        if (student.Teachers.Any())
                        {
                            Console.WriteLine("\tStudent's Teachers:");
                            foreach (var teacher in student.Teachers)
                            {
                                Console.WriteLine($"\t\tTeacher: {teacher}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\tNo teachers found for this student.");
                        }
                        Console.WriteLine("=======================================");
                    }
                    Console.WriteLine("Press Enter to continue.....");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while listing the students: {ex.Message}");
                Console.ReadKey();
            }
        }



        public void FindStudent(string StudentEnrollment) 
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var assignments = (from student in context.Students
                                       join studCourse in context.StudentsCourses
                                       on student.StudentId equals studCourse.StudentID into studentCources
                                       from studCourse in studentCources.DefaultIfEmpty()
                                       join grade in context.Grades.AsNoTracking()
                                       on new { student.StudentId, studCourse.CourseID } equals new { grade.StudentId, grade.CourseID } into studentGrades
                                       from grade in studentGrades.DefaultIfEmpty()
                                       join teacher in context.TeachersStudents.AsNoTracking()
                                       on student.StudentId equals teacher.StudentID into studentTeacher
                                       from teacher in studentTeacher.DefaultIfEmpty()
                                       where student.StudentEnrollment==StudentEnrollment
                                       group new { student, studCourse.Course, grade, teacher.Teacher } by new
                                       {
                                           student.StudentId,
                                           student.StudentEnrollment,
                                           student.StudentPhone,
                                           student.StudentGpa,
                                           student.StudentName,
                                           student.StudenAddress
                                       } into studentGroup
                                       select new
                                       {
                                           StudentId = studentGroup.Key.StudentId,
                                           StudentEnrollment = studentGroup.Key.StudentEnrollment,
                                           StudentPhone = studentGroup.Key.StudentPhone,
                                           StudentGpa = studentGroup.Key.StudentGpa,
                                           StudentName = studentGroup.Key.StudentName,
                                           StudenAddress = studentGroup.Key.StudenAddress,
                                           Courses = studentGroup.Select(g => g.Course.CourseName).Distinct().ToList(),
                                           Grades = studentGroup.Select(g => g.grade.GradeValue).Distinct().ToList(),
                                           Teachers = studentGroup.Select(g => g.Teacher.TeacherName).Distinct().ToList()
                                       }).FirstOrDefault();

                    if (assignments!=null)
                    {
                        Console.WriteLine($"Student {assignments.StudentId}");
                        Console.WriteLine($"\tStudent's Enrollment: {assignments.StudentEnrollment}");
                        Console.WriteLine($"\tStudent's Name: {assignments.StudentName}");
                        Console.WriteLine($"\tStudent's Gpa: {assignments.StudentGpa}");
                        Console.WriteLine($"\tStudent's Address: {assignments.StudenAddress}");
                        Console.WriteLine($"\tStudent's PhoneNumber: {assignments.StudentPhone}");

                        if (assignments.Courses.Count != 0)
                        {
                            Console.WriteLine("\tStudent's Courses and Grades:");
                            for (int i = 0; i < assignments.Courses.Count; i++)
                            {
                                var grade = i < assignments.Grades.Count ? assignments.Grades[i] : "(no grade assigned)";
                                Console.WriteLine($"\t\tCourse: {assignments.Courses[i]} \tGrade: {grade}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\tNo cources registered..");
                        }

                        if (assignments.Teachers.Any())
                        {
                            Console.WriteLine("\tStudent's Teachers:");
                            foreach (var teacher in assignments.Teachers)
                            {
                                Console.WriteLine($"\t\tTeacher: {teacher}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\tNo teachers found for this student.");
                        }
                        Console.WriteLine("=======================================");
                    }
                    Console.WriteLine("Press Enter to continue.....");
                    Console.ReadKey();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while Finding the students: {ex.Message}");
            }

        }
        
        public void RegisterCource(string Code,int Id)
        {
            try
            {
                var input = new StudentCourse();
                using (var context = new AppDbContext())
                {
                    input.StudentID= Id;
                    var cs = context.Courses.Single(e => e.CourseCode == Code);
                    if (cs != null)
                    {
                        input.CourseID= cs.CourseID;
                    }
                    var exists=context.StudentsCourses.Any(e => e.StudentID== Id&&e.CourseID==cs.CourseID);
                    if (!exists)
                    {
                        context.StudentsCourses.Add(input);
                        context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Course already registered.");
                        Console.ReadKey();
                    }
                }
                Console.WriteLine("Student Enrolled Successfully.\n Press Enter to continue...");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while Enrolling the students: {ex.Message}");
            }
        }
    }
}
