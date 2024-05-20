using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModels.Allocation
{
    public class StudentAllocationGroupByStudentViewModel
    {
        [DisplayName("Student Reg No")]
        public string StudentRegNo { get; set; }
        [DisplayName("Student Name")]
        public string StudentName { get; set; }

        public long StudentID { get; set; }

        public bool isEnable {  get; set; }

       

        public IEnumerable<SubjectAllocationGroupByTeacherViewModel> subjectAllocations { get; set; }


    }
}
