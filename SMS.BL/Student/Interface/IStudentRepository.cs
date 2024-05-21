using SMS.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Student.Interface
{
    public interface IStudentRepository
    {
        IEnumerable<StudentBO> GetAllStudents(bool? isActive = null);

        StudentBO GetStudentByID(long id);

        bool DeleteStudent(long id, out string msg);

        bool CheckStudentName(string studentname);

        bool CheckTeacherRegNo(string studentRegNumber);

        bool CheckStudentEmail(string studentEmail);

        bool CheckStudentInUse(long id);

        bool ToggleEnable(long id, bool isEnable, out string msg);

        bool SaveStudent(StudentBO student, out string msg);

        bool UpdatedStudentDetails(StudentBO student, out string msg, Data.Student editStudent);

        IEnumerable<StudentBO> GetSearchStudents(string item, string criteria);
    }
}
