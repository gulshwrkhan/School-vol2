using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModified.Model
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentEnrollment { get; set; }
        public float StudentGpa { get; set; }
        public string StudenAddress { get; set; }
        public string StudentPhone { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } //navigation to many-to-many
        public ICollection<Grade> Grades { get; set; }//navigation to one-to-many
        public ICollection<TeacherStudent> TeacherStudents { get; set; }//navigation to many-to-many
    }
}
