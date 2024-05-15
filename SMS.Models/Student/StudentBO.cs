using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Models.Student
{
    public class StudentBO
    {
        public long StudentID { get; set; }
        [Required(ErrorMessage = "Teacher registration number is required")]
        [DisplayName("Registration No")]
        public string StudentRegNo { get; set; }
        [Required(ErrorMessage = "Teacher First Name is required")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Teacher Last Name is required")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Teacher Display Name is required")]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        [DisplayName("Date Of Birth")]
        public System.DateTime DOB { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [DisplayName("Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Contact No is required")]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        [DisplayName("Status")]
        public bool IsEnable { get; set; }
    }
}
