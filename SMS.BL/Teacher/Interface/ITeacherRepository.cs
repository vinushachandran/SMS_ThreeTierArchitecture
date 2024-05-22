using SMS.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Teacher.Interface
{
    public interface ITeacherRepository
    {
        /// <summary>
        /// Display all the teachers based on active inactive or all filter
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<TeacherBO> GetAllTeacher(bool? isActive = null);

        /// <summary>
        /// Get one teacher by her id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TeacherBO GetTeacherByID(long id);

        /// <summary>
        /// Delete one teacher by her id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool DeleteTeacher(long id, out string msg);

        /// <summary>
        /// Check this teacher display name already use for any other teachers
        /// </summary>
        /// <param name="teacherName"></param>
        /// <returns></returns>
        bool CheckTeacherName(string teacherName);

        /// <summary>
        /// Check this reg no already use for any other teacher
        /// </summary>
        /// <param name="teacherRegNumber"></param>
        /// <returns></returns>
        bool CheckTeacherRegNo(string teacherRegNumber);

        /// <summary>
        /// check this email address alredy used for any other teacher
        /// </summary>
        /// <param name="teacherEmail"></param>
        /// <returns></returns>
        bool CheckTeacherEmail(string teacherEmail);

        /// <summary>
        /// Check this teacher is allocated for any subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckTeacherInUse(long id);

        /// <summary>
        /// handling enable disable button
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool ToggleEnable(long id, bool isEnable, out string msg);

        /// <summary>
        /// Add a new teacher or edit the teacher
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool SaveTeacher(TeacherBO teacher, out string msg);

        /// <summary>
        /// Seaching the teacher details
        /// </summary>
        /// <param name="item"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<TeacherBO> GetSearchTeachers(string item, string criteria);
    }
}
