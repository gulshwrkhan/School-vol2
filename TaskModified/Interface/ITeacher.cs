using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModified.Model;

namespace TaskModified.Interface
{
    public interface ITeacher
    {
        void AddTeacher(Teacher Teachers);
        void RemoveTeachers(int TeacherID);
        void UpdateTeacher(Teacher teacher, int TeacherID);
        void ListAllTeacehers();
        void FindTeacher(int TeacherID);
        void AssignTeacherToStudent();
        void AssignTeacherToCource(int TeacherID,string CourceCode);
        void AssignGrades(string CourceCode,int TeacherID,string enroll,string gd);
    }
}
