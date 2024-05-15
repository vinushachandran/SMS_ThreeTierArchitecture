using SMS.Models.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModels.Subject
{
    /// <summary>
    /// get all subjects as list
    /// </summary>
    public class SubjectViewModels
    {
        public IEnumerable<SubjectBO> Subjects { get; set; }
    }
}
