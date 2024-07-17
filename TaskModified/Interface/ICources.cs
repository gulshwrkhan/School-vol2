using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModified.Model;

namespace TaskModified.Interface
{
    public interface ICources
    {
        void AddCource(Course course);
        void RemoveCource(string code);
        void UpdateCource(Course course, string CourceCode);
        void ListAllCource();
        void FindCource(string CourceCode);
    }
}
