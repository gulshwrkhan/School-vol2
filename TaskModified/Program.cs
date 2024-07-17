using System;
using TaskModified.Controllers;

namespace TaskModified
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                var st=new StudentController();
                var ts=new TeacherController();
                var cs =new CourceController();
                Console.Clear();
                Console.WriteLine("\t\t\t\t Main Menu\n\n");
                Console.WriteLine("Which database you want to use: ");
                Console.WriteLine("1.Student");
                Console.WriteLine("2.Teacher");
                Console.WriteLine("3.Courses");
                Console.WriteLine("4.Exit");
                Console.Write("Enter Your Options: ");
                string option=Console.ReadLine();
                if(option=="1")
                {
                    st.ShowMenu();
                }
                else if(option=="2")
                {
                    ts.ShowMenu();
                }
                else if(option=="3")
                {
                    cs.ShowMenu();
                }
                else if(option=="4")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Wrong Input. Please Enter again.....");
                    Console.ReadKey();
                }
            }
        }
    }
}
