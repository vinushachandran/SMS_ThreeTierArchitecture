using SMS.Models.Allocation;
using SMS.Models.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModels.Allocation
{
    public class AllocationViewModel
    {
        /// <summary>
        /// Get Subject allocations as a list
        /// </summary>
        public IEnumerable<SubjectAllocationGroupByTeacherViewModel> SubjectAllocations { get; set; }
        /// <summary>
        /// Get Students allocations as a list
        /// </summary>
        public IEnumerable<StudentAllocationGroupByStudentViewModel> StudentAllocations { get; set;}
    }
}
