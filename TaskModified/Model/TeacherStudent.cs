using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModified.Model
{
    public class TeacherStudent
    {
        public int StudentID { get; set; }// foriegn key
        public virtual Student Student { get; set; } //navigation
        public int TeacherID { get; set; } //foreign key
        public virtual Teacher Teacher { get; set; } //navigation
    }
}
