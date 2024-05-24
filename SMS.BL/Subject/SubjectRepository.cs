/// <summary>
/// This class represents the subject repository
/// </summary>
/// <author>Vinusha</author>

using SMS.BL.Subject.Interface;
using SMS.Data;
using SMS.Models.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Subject
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SMS_DBEntities _dbEntities;

        public SubjectRepository(SMS_DBEntities DBEntities)
        {
            _dbEntities = DBEntities;
        }

        /// <summary>
        /// Check this subject code already exist
        /// </summary>
        /// <param name="subjectCode"></param>
        /// <returns></returns>

        public bool CheckSubjectCode(string subjectCode)
        {
            bool existingSubjectCode =_dbEntities.Subjects.Any(s => s.SubjectCode == subjectCode);
            return existingSubjectCode;
        }

        /// <summary>
        /// Check this subject allocated for any teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckSubjectInUse(long id)
        {
            bool SubjectInUse = _dbEntities.Teacher_Subject_Allocation.Any(a => a.SubjectID == id);           
            return SubjectInUse;
        }

        /// <summary>
        /// check this subject name already exist
        /// </summary>
        /// <param name="subjectName"></param>
        /// <returns></returns>
        public bool CheckSubjectName(string subjectName)
        {
            bool existingSubjectName =_dbEntities.Subjects.Any(s => s.Name == subjectName);
            return existingSubjectName;
        }

        /// <summary>
        /// Delete one subject by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteSubject(long id, out string msg)
        {
            msg = "";
            var subject = _dbEntities.Subjects.SingleOrDefault(s => s.SubjectID == id);

            try
            {

                if (subject != null)
                {
                    bool isSubjectUsed = _dbEntities.Teacher_Subject_Allocation.Any(s => s.SubjectID == subject.SubjectID);
                    if (isSubjectUsed)
                    {
                        msg = "This subject " + subject.Name + " is in Use.";
                        return false;
                    }
                    _dbEntities.Subjects.Remove(subject);
                    _dbEntities.SaveChanges();
                    return true;
                }
                msg = "Already removed";
                return false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Display all the subjects
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<SubjectBO> GetAllSubject(bool? isActive = null)
        {
            var allSubjects = _dbEntities.Subjects.Select(s => new SubjectBO()
            {
                SubjectID = s.SubjectID,
                SubjectCode = s.SubjectCode,
                Name = s.Name,
                IsEnable = s.IsEnable

            });

            if (isActive.HasValue)
            {
                allSubjects = allSubjects.Where(s => s.IsEnable == isActive.Value);
            }


            return allSubjects;
        }

        /// <summary>
        /// Searching filter by it's subject id and name
        /// </summary>
        /// <param name="item"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<SubjectBO> GetSearchSubjects(string item, string criteria)
        {
            var allCriteria = GetAllSubject();

            if (criteria == "SubjectCode")
            {
                allCriteria = allCriteria.Where(s => s.SubjectCode.ToUpper().Contains(item.ToUpper())).ToList();
            }
            else if (criteria == "Name")
            {
                allCriteria = allCriteria.Where(s => s.Name.ToUpper().Contains(item.ToUpper())).ToList();
            }
            else
            {
                allCriteria = allCriteria.Where(s => s.SubjectCode.ToUpper().Contains(item.ToUpper()) || s.Name.ToUpper().Contains(item.ToUpper())).ToList();
            }
            return allCriteria;
        }

        /// <summary>
        /// Get the one subject details by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SubjectBO GetSubjetByID(long id)
        {
            var subject = _dbEntities.Subjects.Select(s => new SubjectBO()
            {
                SubjectID = s.SubjectID,
                SubjectCode = s.SubjectCode,
                Name = s.Name,
                IsEnable = s.IsEnable
            }).Where(s => s.SubjectID == id).FirstOrDefault();
            return subject;
        }

        /// <summary>
        /// To save or edit the subject details
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SaveSubject(SubjectBO subject, out string msg)
        {
            msg = "";

            bool existingSubject = _dbEntities.Subjects.Any(s => s.SubjectID == subject.SubjectID);

            bool SubjectInUse = CheckSubjectInUse(subject.SubjectID);
            bool isSubCodeAvailable = CheckSubjectCode(subject.SubjectCode);
            bool isSubNameAvailable = CheckSubjectName(subject.Name);
            try
            {
                if (existingSubject)
                {
                    if (SubjectInUse)
                    {
                        msg = "The Subject " + subject.Name + " is Followed by a sudent";
                        return false;
                    }
                    var editSubjet = _dbEntities.Subjects.SingleOrDefault(s => s.SubjectID == subject.SubjectID);

                    if (editSubjet == null)
                    {
                        msg = "Unable to find the subject for edit";
                        return false;
                    }

                    isSubCodeAvailable =  _dbEntities.Subjects.Any(s => s.SubjectCode == subject.SubjectCode && s.SubjectID != subject.SubjectID);
                    isSubNameAvailable = _dbEntities.Subjects.Any(s => s.Name == subject.Name && s.SubjectID != subject.SubjectID);


                    if (isSubCodeAvailable)
                    {
                        msg = "Subject code already exists.";
                        return false;
                    }

                    if (isSubNameAvailable)
                    {
                        msg = "Subject Name already exists.";
                        return false;
                    }

                    editSubjet.SubjectCode = subject.SubjectCode;
                    editSubjet.Name = subject.Name;
                    editSubjet.IsEnable = subject.IsEnable;
                    _dbEntities.SaveChanges();
                    msg = "Subject Updated Successfully!";
                    return true;

                }
                if (!isSubCodeAvailable)
                {
                    msg = "Subject code already exists.";
                    return false;
                }

                if (!isSubNameAvailable)
                {
                    msg = "Subject Name already exists.";
                    return false;
                }
                var newSubject = new Data.Subject();
                newSubject.SubjectCode = subject.SubjectCode;
                newSubject.Name = subject.Name;
                newSubject.IsEnable = subject.IsEnable;
                _dbEntities.Subjects.Add(newSubject);
                _dbEntities.SaveChanges();
                msg = "Subject Added Successfully!";
                return true;
            }
            catch (Exception error)
            {
                msg = error.Message;
                return false;
            }
        }

        /// <summary>
        /// To handle enable desable button
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ToggleEnable(long id, bool isEnable, out string msg)
        {
            var subject = _dbEntities.Subjects.SingleOrDefault(s => s.SubjectID == id);
            if (subject != null)
            {
                if (isEnable)
                {
                    subject.IsEnable = true;
                    _dbEntities.SaveChanges();
                    msg = "This subject " + subject.Name + " successfully enabled! ";
                    return true;
                }
                else
                {
                    if (CheckSubjectInUse(id))
                    {
                        msg = "This Subject " + subject.Name + " Allocated for a teacher you can't disable it!";
                        return false;
                    }
                    else
                    {
                        subject.IsEnable = false;
                        _dbEntities.SaveChanges();
                        msg = "This subject " + subject.Name + " successfully disabled! ";
                        return true;
                    }
                }
            }
            else
            {
                msg = "Subject not found !";
                return false;
            }
        }
    }
}
