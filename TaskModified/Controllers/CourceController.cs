using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModified.Data;
using TaskModified.Interface;
using TaskModified.Model;
using TaskModified.Services;

namespace TaskModified.Controllers
{
    public  class CourceController
    {
        public void ShowMenu()
        {
            var cs=new CourceServices();
            while(true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Course Menu");
                Console.WriteLine("What functionality you want to do with the database: ");
                Console.WriteLine("1.Add. ");
                Console.WriteLine("2.Remove. ");
                Console.WriteLine("3.Update. ");
                Console.WriteLine("4.List All Course. ");
                Console.WriteLine("5.Find One Course.");
                Console.WriteLine("6.Main Menu. ");
                Console.Write("\t Enter your option: ");
                string option=Console.ReadLine();
                if(option=="1")
                {
                    AddCource(cs);
                }
                else if(option=="2")
                {
                    RemoveCource(cs);
                }
                else if(option=="3")
                {
                    UpdateCource(cs);
                }
                else if(option=="4")
                {
                    cs.ListAllCource();
                }
                else if(option== "5")
                {
                    FindCource(cs);
                }
                else if(option== "6")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please Enter correct input....");
                    Console.ReadKey();
                }
            }
        }

        static void FindCource(CourceServices cs)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Find Cource\n\n");
            Console.Write("Enter the Cource's Code to find: ");
            string code;
            do
            {
                code = Console.ReadLine();
            } while (code == null);
            cs.FindCource(code);
        }

        static void UpdateCource(CourceServices cs)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Update Cource\n\n");
                Console.WriteLine("Which cource you want to update: ");
                using(var context=new AppDbContext())
                {
                    var cources = context.Courses.ToList();
                    foreach(var i in cources)
                    {
                        Console.WriteLine($"\tCource Name: {i.CourseName}\t Cource Code: {i.CourseCode}");
                    }
                    string code;
                    do
                    {
                        code = Console.ReadLine();
                    } while (code != null);
                    var input = new Course();
                    Console.Write("Enter the name of cource: ");
                    input.CourseName = Console.ReadLine();
                    Console.Write("Enter the code of cource: ");
                    input.CourseCode = Console.ReadLine();
                    cs.UpdateCource(input, code);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An Error Occurred:{ex.Message}");
            }
        }

        static void AddCource(CourceServices cs)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t Add Cource\n\n");
            var cource = new Course();
            Console.Write("Enter the name of cource: ");
            cource.CourseName = Console.ReadLine();
            Console.Write("Enter the code of cource: ");
            cource.CourseCode = Console.ReadLine();
            
            cs.AddCource(cource);

        }

        static void RemoveCource(CourceServices cs)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Remove Cource\n\n");
                Console.WriteLine("Which cource you want to remove: ");
                using (var context = new AppDbContext())
                {
                    var cource = context.Courses.ToList();
                    foreach(var i in cource)
                    {
                        Console.WriteLine($"\t Couce Name's: {i.CourseName}\t Cource Code: {i.CourseCode}");
                    }
                    Console.Write("Write cource code to delete the cource: ");
                    string code;
                    do
                    {
                        code = Console.ReadLine();
                    }while(code == null);
                    cs.RemoveCource(code);
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"An Error Occurred:{ex.Message}");
            }
        }
    }
}
