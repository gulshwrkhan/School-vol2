using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModified.Data;
using TaskModified.Interface;
using TaskModified.Model;

namespace TaskModified.Services
{
    public class CourceServices:ICources
    {
        public void AddCource(Course course)
        {
            try
            {
                using (var context=new AppDbContext())
                {
                    context.Courses.Add(course);
                    context.SaveChanges();
                }
                Console.WriteLine("Cource Added Successfully.\n Press Enter to continue...");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while Adding the cource: {ex.Message}");
                Console.ReadKey();
            }
        }
        public void RemoveCource(string code)
        {
            try
            {
                using (var context=new AppDbContext())
                {
                    var cs=context.Courses.Single(e=>e.CourseCode==code);
                    context.Courses.Remove(cs);
                    context.SaveChanges();
                }
                Console.WriteLine("Cource removed Successfully.\n Press Enter to continue...");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while removing the cource: {ex.Message}");
            }
        }
        public void UpdateCource(Course course, string code)
        {
            try
            {
                using(var context=new AppDbContext())
                {
                    var cs = context.Courses.Single(e => e.CourseCode == code);
                    cs.CourseName = course.CourseName;
                    cs.CourseCode=course.CourseCode;
                    context.SaveChanges();
                }
                Console.WriteLine("Cource updated Successfully.\n Press Enter to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the cource: {ex.Message}");
            }
        }
        public void ListAllCource()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Listing the cources\n\n");
                using(var context=new AppDbContext())
                {
                    var cs = context.Courses.ToList();
                    foreach(var i in cs)
                    {
                        Console.WriteLine($"\t Cource Name: {i.CourseName}\t Cource Code: {i.CourseCode}");
                        Console.WriteLine("======================================");
                    }
                }
                Console.WriteLine("Press Enter to continue.....");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while listing all the cources: {ex.Message}");

            }
        }
        public void FindCource(string code)
        {
            try
            {
                using(var context=new AppDbContext())
                {
                    var cs = context.Courses.Single(e => e.CourseCode==code);
                    Console.WriteLine($"\t Cource Name: {cs.CourseName}\t Cource Code: {cs.CourseCode}");
                    Console.WriteLine("======================================");
                }
                Console.WriteLine("Press Enter to continue.....");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while finding the cource: {ex.Message}");
            }
        }
    }
}
