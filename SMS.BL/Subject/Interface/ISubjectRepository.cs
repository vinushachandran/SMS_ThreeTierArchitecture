/// <summary>
/// This class represents the subject interface
/// </summary>
/// <author>Vinusha</author>


using SMS.Models.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Subject.Interface
{
    public interface ISubjectRepository
    {
        /// <summary>
        /// Display all the subject
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<SubjectBO> GetAllSubject(bool? isActive = null);

        /// <summary>
        /// Get one subject by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SubjectBO GetSubjetByID(long id);

        /// <summary>
        /// Delete the subject
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool DeleteSubject(long id, out string msg);

        /// <summary>
        /// add a new subject or edit the exsiting subject
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool SaveSubject(SubjectBO subject, out string msg);

        /// <summary>
        /// check the subject is allocated for any teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckSubjectInUse(long id);

        /// <summary>
        /// Check the subject name already use forr another subject
        /// </summary>
        /// <param name="subjectName"></param>
        /// <returns></returns>
        bool CheckSubjectName(string subjectName);

        /// <summary>
        /// Check the subject cide already use forr another subject
        /// </summary>
        /// <param name="subjectCode"></param>
        /// <returns></returns>
        bool CheckSubjectCode(string subjectCode);

        /// <summary>
        /// Handle the enable disable button
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool ToggleEnable(long id, bool isEnable, out string msg);

        /// <summary>
        /// Searching subject
        /// </summary>
        /// <param name="item"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<SubjectBO> GetSearchSubjects(string item, string criteria);
    }
}
