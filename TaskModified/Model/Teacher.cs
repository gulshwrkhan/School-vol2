using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModified.Model
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int TeacherSalary { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherPhoneNumber { get; set; }
        public ICollection<TeacherStudent> TeacherStudents { get; set; }//navigation to many-to-many
        public ICollection<Course>Courses { get; set; } //navigation to one-to-many
    }
}
