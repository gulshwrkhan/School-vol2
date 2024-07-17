using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TaskModified.Data;
using TaskModified.Model;
using TaskModified.Services;

namespace TaskModified.Controllers
{
    public class StudentController
    {
        public void ShowMenu()
        {
            var stService=new StudentService();
            while(true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Student Menu\n\n\n");
                Console.WriteLine("What functionality you want to do with the database: ");
                Console.WriteLine("1.Add. ");
                Console.WriteLine("2.Remove. ");
                Console.WriteLine("3.Update. ");
                Console.WriteLine("4.List All Students. ");
                Console.WriteLine("5.Find One Student.");
                Console.WriteLine("6.Register Course.");
                Console.WriteLine("7.Main Menu. ");
                Console.Write("\t Enter your option: ");
                string option= Console.ReadLine();
                if(option=="1")
                {
                    AddStudents(stService);
                }
                else if(option=="2")
                {
                    RemoveStudents(stService);
                }
                else if(option=="3")
                {
                    UpdateStudents(stService);
                }
                else if(option=="4")
                {
                    stService.ListAllStudents();
                }
                else if(option=="5")
                {
                    FindStudent(stService);
                }
                else if(option=="6")
                {
                    RegisterCource(stService);
                }
                else if(option=="7")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong input.PLease enter again.......");
                    Console.ReadKey();
                }
            }
            
        }

        static void RegisterCource(StudentService stService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Register Cource\n\n");
                using(var context=new AppDbContext())
                {
                    var st=context.Students.ToList();
                    var cs=context.Courses.ToList();
                    Console.WriteLine("Which Student you want to enroll: ");
                    foreach(var i in st)
                    {
                        Console.WriteLine($"\tStudent id: {i.StudentId}\tStudent Enrollment: {i.StudentEnrollment}");
                    }
                    Console.Write("Enter your option: ");
                    int id;
                    do
                    {
                        id = StringToInt(Console.ReadLine());
                    } while (id==0);
                    Console.WriteLine("Which Cource You want to Register: ");
                    foreach(var i in cs)
                    {
                        Console.WriteLine($"\tCource Code: {i.CourseCode}\t Cource Name: {i.CourseName}");
                    }
                    Console.Write("Enter your option: ");
                    string Code;
                    do
                    {
                        Code=Console.ReadLine();
                    }while(Code==null);
                    stService.RegisterCource(Code,id);
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"An Error Occurred:{ex.Message}");
            }
        }

        static void AddStudents(StudentService stService)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Adding Students\n\n");
            var st = new Student();
            Console.Write("Enter Student's name: ");
            st.StudentName = Console.ReadLine();
            Console.Write("Enter Student's Enrollment: ");
            st.StudentEnrollment = Console.ReadLine();
            do 
            {
                Console.Write("Enter Student's Gpa: ");
                st.StudentGpa = StringTofloat(Console.ReadLine());
            } while (st.StudentGpa==0);
            
            Console.Write("Enter Student's Address: ");
            st.StudenAddress = Console.ReadLine();
            Console.Write("Enter Student's Number: ");
            st.StudentPhone = Console.ReadLine();
            stService.AddStudent(st);
        }

        static void RemoveStudents(StudentService stService)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Removing Students\n\n");
            try
            {
                using (var context = new AppDbContext())
                {
                    var st = context.Students.ToList();
                    foreach (var i in st)
                    {
                        Console.WriteLine("Student Enrollment " + i.StudentEnrollment + " : " + i.StudentName);
                    }
                    Console.Write("Which Student you want to delete: ");
                    string id = Console.ReadLine();
                    stService.RemoveStudent(id);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An Error Occurred:{ex.Message}");
            }
        }

        static void UpdateStudents(StudentService stService)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Updating Students\n\n");
            try
            {
                using(var context=new AppDbContext())
                {
                    var st = context.Students.ToList();
                    foreach (var i in st)
                    {
                        Console.WriteLine("Student Enrollment " + i.StudentEnrollment + " : " + i.StudentName);
                    }
                    Console.Write("Which Student you want to update: ");
                    string id = Console.ReadLine();
                    var up = new Student();
                    Console.Write("Enter Student's name: ");
                    up.StudentName = Console.ReadLine();
                    Console.Write("Enter Student's Enrollment: ");
                    up.StudentEnrollment = Console.ReadLine();
                    Console.Write("Enter Student's Gpa: ");
                    up.StudentGpa = float.Parse(Console.ReadLine());
                    Console.Write("Enter Student's Address: ");
                    up.StudenAddress = Console.ReadLine();
                    Console.Write("Enter Student's Number: ");
                    up.StudentPhone = Console.ReadLine();
                    stService.UpdateStudent(up, id);
                }
            }
            catch(Exception ex )
            {
                Console.WriteLine($"An Error Occurred:{ex.Message}");
            }
        }

        static void FindStudent(StudentService service)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\tFinding Student\n\n");
            Console.Write("Enter the student enrollment to retreive: ");
            string id = Console.ReadLine();
            service.FindStudent(id);
        }

        static float StringTofloat(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                Console.WriteLine("Please enter a correct input.");
                Console.ReadKey();
                return 0;
            }
            else if (float.TryParse(s, out float result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Please enter a valid numeric input.");
                Console.ReadKey();
                return 0;
            }
        }

        static int StringToInt(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                Console.WriteLine("Please enter a correct input.");
                Console.ReadKey();
                return 0;
            }
            else if (int.TryParse(s, out int result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Please enter a valid numeric input.");
                Console.ReadKey();
                return 0; 
            }
        }
    }
}
