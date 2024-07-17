using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TaskModified.Data;
using TaskModified.Model;
using TaskModified.Services;

namespace TaskModified.Controllers
{
    public class TeacherController
    {
        public void ShowMenu()
        {
            TeacherService ts = new TeacherService();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Teacher Menu\n\n");
                Console.WriteLine("What functionality you want to do with the database: ");
                Console.WriteLine("1.Add. ");
                Console.WriteLine("2.Remove. ");
                Console.WriteLine("3.Update. ");
                Console.WriteLine("4.List All Teacher. ");
                Console.WriteLine("5.Find One Teacher.");
                Console.WriteLine("6.Assign Teacher to Course.");
                Console.WriteLine("7.Assign Teacher to Students.");
                Console.WriteLine("8.Add Grade.");
                Console.WriteLine("9.Main Menu. ");
                Console.Write("\t Enter your option: ");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    AddTeacher(ts);
                }
                else if (option == "2")
                {
                    RemoveTeacher(ts);
                }
                else if (option == "3")
                {
                    UpdateTeacher(ts);
                }
                else if (option == "4")
                {
                    ts.ListAllTeacehers();
                }
                else if (option == "5")
                {
                    FindTeacher(ts);
                }
                else if (option == "6")
                {
                    AssignTeacherToCource(ts);
                }
                else if (option == "8")
                {
                    AssignGrades(ts);
                }
                else if (option == "7")
                {
                    ts.AssignTeacherToStudent();
                }
                else if (option == "9")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong input. Please enter correct input.....");
                    Console.ReadKey();
                }
            }
        }

        

        static void AssignGrades(TeacherService ts)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Assigning Grades to Students\n\n");
                Console.Write("Please Enter your Teacher's ID to grade: ");
                int id;
                do
                {
                    id=StringToInt(Console.ReadLine());
                }while(id==0);
                using (var context = new AppDbContext())
                {
                    var t = context.Teachers.Any(e => e.TeacherId == id);
                    if (t)
                    {
                        Console.Write("Please Enter Cource Code to grade: ");
                        string code;
                        do
                        {
                            code = Console.ReadLine();
                        } while (code == "");
                        Console.Write("Please Enter the enrollment of Student to mark grade: ");
                        string enroll;
                        do
                        {
                            enroll = Console.ReadLine();
                        } while (enroll == null);
                        Console.Write("Please Enter the grade of Student to mark: ");
                        string gd;
                        do
                        {
                            gd = Console.ReadLine();
                        } while (gd == null);
                        ts.AssignGrades(code, id, enroll, gd);
                    }
                    else
                    {
                        Console.WriteLine("No Teacher at this id.....");
                        Console.ReadKey();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An Error Occurred:{ex.Message}");
                Console.ReadKey();
            }
        }

        static void AssignTeacherToCource(TeacherService ts)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Assigning Teacher to Cources\n\n");
                using (var context=new AppDbContext())
                {
                    var t=context.Teachers.ToList();
                    var c=context.Courses.ToList();
                    Console.WriteLine("Which Teacher You want to Assign the cource: ");
                    foreach(var i in t)
                    {
                        Console.WriteLine($"\tTeacher's Id:{i.TeacherId}\tTeacher's Name: {i.TeacherName}");
                    }
                    Console.Write("Enter your option: ");
                    int tId;
                    do
                    {
                        tId=StringToInt(Console.ReadLine());
                    }while(tId==0);
                    Console.WriteLine("Which Cource you want to be assigned: ");
                    foreach(var i in c)
                    {
                        Console.WriteLine($"\tCource Name: {i.CourseName}\tCource Code:{i.CourseCode}");
                    }
                    Console.Write("Enter your option: ");
                    string cID;
                    do
                    {
                        cID = Console.ReadLine();
                    } while (cID == null);
                    ts.AssignTeacherToCource(tId, cID);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An Error Occurred:{ex.Message}");
            }
        }

        static void FindTeacher(TeacherService ts)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Find Teacher\n\n");
            Console.Write("Enter the Teacher's ID to find: ");
            int id;
            do
            {
                id = StringToInt(Console.ReadLine());
            } while (id == 0);
            ts.FindTeacher(id);
        }

        static void AddTeacher(TeacherService ts)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Add Teacher\n\n");
            var t = new Teacher();
            Console.Write("Enter Teacher's name: ");
            t.TeacherName = Console.ReadLine();
            Console.Write("Enter Teacher's Phone Number: ");
            t.TeacherPhoneNumber = Console.ReadLine();
            Console.Write("Enter Teacher Salary: ");
            t.TeacherSalary = int.Parse(Console.ReadLine());
            Console.Write("Enter Teacher's Address: ");
            t.TeacherAddress = Console.ReadLine();
            ts.AddTeacher(t);
        }

        static void RemoveTeacher(TeacherService ts)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Remove Teacher\n\n");
            try
            {
                using (var context = new AppDbContext())
                {
                    var t = context.Teachers.ToList();
                    Console.WriteLine("Which Teacher you want to remove: ");
                    foreach (var i in t)
                    {
                        Console.WriteLine($"\t Teacher's ID: {i.TeacherId}\t Teacher's Name: {i.TeacherName}");
                    }
                    Console.Write("Enter Teacher's Id: ");
                    int id;
                    do
                    {
                        id = StringToInt(Console.ReadLine());
                    } while (id == null);
                    ts.RemoveTeachers(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Error Occurred:{ex.Message}");
            }

        }

        static void UpdateTeacher(TeacherService ts)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Remove Teacher\n\n");
                Console.WriteLine("Which Teacher you want to update: ");
                using (var context = new AppDbContext())
                {
                    var ut = context.Teachers.ToList();
                    foreach (var i in ut)
                    {
                        Console.WriteLine($"\tTeacher's ID: {i.TeacherId}\tTeacher's Name: {i.TeacherName}");
                    }
                    Console.Write("Enter the Teacher's ID: ");
                    int id;
                    do
                    {
                        id = StringToInt(Console.ReadLine());
                    } while (id == 0);
                    var t = new Teacher();
                    Console.Write("Enter Teacher's name: ");
                    t.TeacherName = Console.ReadLine();
                    Console.Write("Enter Teacher's Phone Number: ");
                    t.TeacherPhoneNumber = Console.ReadLine();
                    Console.Write("Enter Teacher Salary: ");
                    t.TeacherSalary = int.Parse(Console.ReadLine());
                    Console.Write("Enter Teacher's Address: ");
                    t.TeacherAddress = Console.ReadLine();
                    ts.UpdateTeacher(t, id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Error Occurred:{ex.Message}");
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
