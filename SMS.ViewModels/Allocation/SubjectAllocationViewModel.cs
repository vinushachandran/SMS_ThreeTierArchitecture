using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModels.Allocation
{
    public class SubjectAllocationViewModel
    {
        
        public long SubjectAllocationID { get; set; }

        [DisplayName("Subject Code")]
        public  string SubjectCode {  get; set; }

        [DisplayName("Subject Name")]
        public string SubjectName { get; set; }

        public long StudentAllocationID { get; set; }

        public string TeacherRegNo { get; set; }
    }
}
