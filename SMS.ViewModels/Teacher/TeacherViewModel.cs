using SMS.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModels.Teacher
{
    /// <summary>
    /// get all teachers as list
    /// </summary>
    public class TeacherViewModel
    {
        public IEnumerable<TeacherBO> Teachers { get; set; }
    }
}
