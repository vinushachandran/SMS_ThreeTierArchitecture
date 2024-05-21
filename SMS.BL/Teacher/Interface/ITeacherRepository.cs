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
        IEnumerable<TeacherBO> GetAllTeacher(bool? isActive = null);

        TeacherBO GetTeacherByID(long id);

        bool DeleteTeacher(long id, out string msg);

        bool CheckTeacherName(string teacherName);

        bool CheckTeacherRegNo(string teacherRegNumber);

        bool CheckTeacherEmail(string teacherEmail);

        bool CheckTeacherInUse(long id);

        bool ToggleEnable(long id, bool isEnable, out string msg);

        bool SaveTeacher(TeacherBO teacher, out string msg);

        IEnumerable<TeacherBO> GetSearchTeachers(string item, string criteria);
    }
}
