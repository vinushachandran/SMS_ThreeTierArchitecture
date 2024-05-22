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
        /// <summary>
        /// Show all the details about the studentsor enabled student or disabled students
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<StudentBO> GetAllStudents(bool? isActive = null);

        /// <summary>
        /// Get the student details bu the student id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StudentBO GetStudentByID(long id);

        /// <summary>
        /// Delete the student details by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool DeleteStudent(long id, out string msg);

        /// <summary>
        /// Check this student name is already used or not
        /// </summary>
        /// <param name="studentname"></param>
        /// <returns></returns>
        bool CheckStudentName(string studentname);

        /// <summary>
        /// Check this student reg no already used or not
        /// </summary>
        /// <param name="studentRegNumber"></param>
        /// <returns></returns>
        bool CheckStudentRegNo(string studentRegNumber);

        /// <summary>
        /// Check this student email address already in used or not
        /// </summary>
        /// <param name="studentEmail"></param>
        /// <returns></returns>
        bool CheckStudentEmail(string studentEmail);

        /// <summary>
        /// Check on this student allocated for any subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckStudentInUse(long id);

        /// <summary>
        /// cheage the active and inactive buttons
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool ToggleEnable(long id, bool isEnable, out string msg);

        /// <summary>
        /// Add a new student or edit exsiting student
        /// </summary>
        /// <param name="student"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool SaveStudent(StudentBO student, out string msg);

        /// <summary>
        /// Editing the student details
        /// </summary>
        /// <param name="student"></param>
        /// <param name="msg"></param>
        /// <param name="editStudent"></param>
        /// <returns></returns>
        bool UpdatedStudentDetails(StudentBO student, out string msg, Data.Student editStudent);

        /// <summary>
        /// Searching the student
        /// </summary>
        /// <param name="item"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<StudentBO> GetSearchStudents(string item, string criteria);
    }
}
