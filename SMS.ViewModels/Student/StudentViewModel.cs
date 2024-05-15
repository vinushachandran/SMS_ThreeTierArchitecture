using SMS.Models.Student;
using SMS.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModels.Student
{
    public class StudentViewModel
    {
        /// <summary>
        /// get all students as list
        /// </summary>
        public IEnumerable<StudentBO> Students { get; set; }
    }
}
