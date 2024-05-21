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
        IEnumerable<SubjectBO> GetAllSubject(bool? isActive = null);

        SubjectBO GetSubjetByID(long id);

        bool DeleteSubject(long id, out string msg);

        bool SaveSubject(SubjectBO subject, out string msg);

        bool CheckSubjectInUse(long id);

        bool CheckSubjectName(string subjectName);

        bool CheckSubjectCode(string subjectCode);

        bool ToggleEnable(long id, bool isEnable, out string msg);

        IEnumerable<SubjectBO> GetSearchSubjects(string item, string criteria);
    }
}
