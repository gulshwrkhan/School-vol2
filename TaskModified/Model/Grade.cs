using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModified.Model
{
    public class Grade
    {
        public int GradeID { get; set; }
        [StringLength(3)]
        public string GradeValue { get; set; }
        public int CourseID { get; set; } //foreign key
        public Course Courses { get; set;} //navigation to one-to-many
        public int StudentId { get; set; } //foreign key
        public Student Students { get; set; } // navigation to one-to-many
    }
}
