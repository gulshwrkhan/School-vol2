using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModified.Model;

namespace TaskModified.Interface
{
    public interface IStudent
    {
        void AddStudent(Student Students);
        void RemoveStudent(string StudentEnrollment);
        void UpdateStudent(Student Student,string StudentEnrollment);
        void ListAllStudents();
        void FindStudent(string StudentEnrollment);
        void RegisterCource(string CourceCode,int StudentId);
    }
}
