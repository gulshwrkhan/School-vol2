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
    public class TeacherService : ITeacher
    {
        public void AddTeacher(Teacher Teachers)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Teachers.Add(Teachers);
                    context.SaveChanges();
                }
                Console.WriteLine("Teacher Added Successfully.\n Press Enter to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while Adding the Teacher: {ex.Message}");
            }
        }
        public void RemoveTeachers(int TeacherID)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var ls = context.Teachers.Single(e => e.TeacherId == TeacherID);
                    context.Teachers.Remove(ls);
                    context.SaveChanges();
                }
                Console.WriteLine("Teacher removed Successfully.\n Press Enter to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing the Teacher: {ex.Message}");
            }
        }
        public void UpdateTeacher(Teacher teacher, int ID)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var t = context.Teachers.Single(e => e.TeacherId == ID);
                    t.TeacherName = teacher.TeacherName;
                    t.TeacherPhoneNumber = teacher.TeacherPhoneNumber;
                    t.TeacherSalary = teacher.TeacherSalary;
                    t.TeacherAddress = teacher.TeacherAddress;
                    context.SaveChanges();
                }
                Console.WriteLine("Teacher Updated Successfully.\n Press Enter to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the Teacher: {ex.Message}");
            }
        }
        public void ListAllTeacehers()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Listing Teachers\n\n");
                using (var context = new AppDbContext())
                {
                    var assignment=(from teacher in context.Teachers
                                    join cource in context.Courses
                                    on teacher.TeacherId equals cource.TeacherId into teacherCource
                                    from cource in teacherCource.DefaultIfEmpty()
                                    group new{ teacher,cource}by new
                                    {
                                        teacher.TeacherId,
                                        teacher.TeacherName,
                                        teacher.TeacherSalary,
                                        teacher.TeacherAddress,
                                        teacher.TeacherPhoneNumber
                                    }into teacherCource
                                    select new
                                    {
                                        teacherID=teacherCource.Key.TeacherId,
                                        teacherNames=teacherCource.Key.TeacherName,
                                        teacherSalary=teacherCource.Key.TeacherSalary,
                                        teacherAddress=teacherCource.Key.TeacherAddress,
                                        teacherPhoneNumber=teacherCource.Key.TeacherPhoneNumber,
                                        Cources=teacherCource.Select(e=>e.cource.CourseName).Distinct().ToList(),   
                                    }).ToList();
                    
                    foreach (var i in assignment)
                    {
                        Console.WriteLine("Teacher's id: " + i.teacherID);
                        Console.WriteLine("\tTeacher's Name: " + i.teacherNames);
                        Console.WriteLine("\tTeacher's Salary: " + i.teacherSalary);
                        Console.WriteLine("\tTeacher's Adress: " + i.teacherAddress);
                        Console.WriteLine("\tTeacher's PhoneNumber: " + i.teacherPhoneNumber);
                        if (i.Cources.Count > 0)
                        {
                            foreach (var l in i.Cources)
                            {
                                Console.WriteLine("\tTeacher's Courses : " + l);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\t No cources assigned.");
                        }
                        Console.WriteLine("======================================");
                    }
                    Console.WriteLine("Press Enter to continue.....");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while Listing the Teacher: {ex.Message}");
            }
        }
        public void FindTeacher(int TeacherID)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var assignment = (from teacher in context.Teachers
                                      join cource in context.Courses
                                      on teacher.TeacherId equals cource.TeacherId into teacherCource
                                      from cource in teacherCource.DefaultIfEmpty()
                                      where teacher.TeacherId==TeacherID
                                      group new { teacher, cource } by new
                                      {
                                          teacher.TeacherId,
                                          teacher.TeacherName,
                                          teacher.TeacherSalary,
                                          teacher.TeacherAddress,
                                          teacher.TeacherPhoneNumber
                                      } into teacherCource
                                      select new
                                      {
                                          teacherID = teacherCource.Key.TeacherId,
                                          teacherNames = teacherCource.Key.TeacherName,
                                          teacherSalary = teacherCource.Key.TeacherSalary,
                                          teacherAddress = teacherCource.Key.TeacherAddress,
                                          teacherPhoneNumber = teacherCource.Key.TeacherPhoneNumber,
                                          Cources = teacherCource.Select(e => e.cource.CourseName).Distinct().ToList(),
                                      }).ToList();

                    foreach (var i in assignment)
                    {
                        Console.WriteLine("Teacher's id: " + i.teacherID);
                        Console.WriteLine("\tTeacher's Name: " + i.teacherNames);
                        Console.WriteLine("\tTeacher's Salary: " + i.teacherSalary);
                        Console.WriteLine("\tTeacher's Adress: " + i.teacherAddress);
                        Console.WriteLine("\tTeacher's PhoneNumber: " + i.teacherPhoneNumber);
                        if(i.Cources.Count > 0)
                        {
                            foreach (var l in i.Cources)
                            {
                                Console.WriteLine("\tTeacher's Courses : " + l);
                            }
                            Console.WriteLine("======================================");
                        }
                        else
                        {
                            Console.WriteLine("\t No cources assigned.");
                        }
                    }
                }
                Console.WriteLine("Press Enter to continue.....");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while Finding the Teacher: {ex.Message}");
            }
        }

        public void AssignTeacherToStudent()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Assigning Teachers to Students\n\n");

                using (var context = new AppDbContext())
                {
                    var assignments = (from student in context.Students.AsNoTracking()
                                      join studentcourse in context.StudentsCourses.AsNoTracking()
                                      on student.StudentId equals studentcourse.StudentID 
                                      join course in context.Courses 
                                      on studentcourse.CourseID equals course.CourseID 
                                      select new { student.StudentId, course.TeacherId, course.CourseID })
                                      .ToList();
                    var teacherStudentAssignments = new List<TeacherStudent>();

                    foreach (var assignment in assignments)
                    {

                        var exists = context.TeachersStudents
                                            .Any(ts => ts.TeacherID == assignment.TeacherId && ts.StudentID == assignment.StudentId);
                        if (!exists)
                        {
                            var teacherStudent = new TeacherStudent
                            {
                                StudentID = assignment.StudentId,
                                TeacherID = assignment.TeacherId.HasValue ? assignment.TeacherId.Value : 0 
                            };

                            teacherStudentAssignments.Add(teacherStudent);
                        }
                    }

                    context.TeachersStudents.AddRange(teacherStudentAssignments);
                    context.SaveChanges();

                    var output = context.TeachersStudents
                                        .Include(ts => ts.Teacher)
                                        .Include(ts => ts.Student)
                                        .ToList();

                    Console.WriteLine("The Teachers assigned to students are: ");
                    foreach (var ts in output)
                    {
                        Console.WriteLine($"\t Teacher Name: {ts.Teacher.TeacherName}\t Student Name: {ts.Student.StudentName}");
                    }
                }

                Console.WriteLine("Press Enter to continue.......");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while Allocating Teachers to Students: {ex.Message}");
                Console.ReadKey();
            }
        }


        public void AssignTeacherToCource(int ID, string Code)
        {
            try
            {
                using(var context = new AppDbContext()) 
                { 
                    var exists=context.Courses.Any(e=>e.TeacherId==ID&&e.CourseCode==Code);
                    if(!exists)
                    {
                        var t=context.Courses.Single(e=>e.CourseCode== Code);
                        t.TeacherId= ID;
                        context.SaveChanges();
                    }
                }
                Console.WriteLine("Cource Assigned Successfully.\n Press Enter to continue...");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while Assigning the Teacher to cources: {ex.Message}");
            }
        }
        public void AssignGrades(string code, int id,string enroll,string gd)
        {
            try
            {
                using(var context=new AppDbContext())
                {
                    var gr = new Grade();
                    var cs=context.Courses.Single(e=>e.CourseCode==code);
                    var st=context.Students.Single(e=>e.StudentEnrollment==enroll);
                    var gs=context.Grades.Any(e=>e.StudentId==st.StudentId&&e.CourseID==cs.CourseID);
                    if(!gs)
                    {
                        gr.GradeValue = gd;
                        gr.StudentId = st.StudentId;
                        gr.CourseID = cs.CourseID;
                        context.Grades.Add(gr);
                        context.SaveChanges();
                    }
                }
                Console.WriteLine("Grade Assigned Successfully.\n Press Enter to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while Assigning the grade to cources: {ex.Message}");
            }
        }
    }
}
