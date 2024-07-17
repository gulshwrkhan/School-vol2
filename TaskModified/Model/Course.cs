using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModified.Model
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get;set; }
        public ICollection<Grade> Grades { get; set; } //navigation to one-many
        public int? TeacherId { get; set; }// foreign key
        public Teacher Teachers { get; set; } //navigation to one-to-many
        public ICollection<StudentCourse> StudentCourses { get; set; }//navigation to many-to-many
    }
}
