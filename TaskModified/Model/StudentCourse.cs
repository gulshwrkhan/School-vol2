using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModified.Model
{
    public class StudentCourse
    {
        public int StudentID { get; set; }// foriegn key
        public virtual Student Student { get; set; } //navigation
        public int CourseID { get; set; }// foriegn key
        public virtual Course Course { get; set; }//navigation
    }
}
