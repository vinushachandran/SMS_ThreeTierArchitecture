using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModels.Allocation
{
    public class StudentAllocationViewModel
    {
        public long StudentAllocationID {  get; set; }

        public IEnumerable<SubjectAllocationGroupByTeacherViewModel> SubjectAllocation {  get; set; }
    }
}
