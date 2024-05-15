using SMS.Data;
using SMS.Models.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SMS.BL
{
    
    public class SubjectBL: SMS_DBEntities
    {
        /// <summary>
        /// Gell all subject list
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<SubjectBO> GetAllSubject(bool? isActive=null)
        {
            var allSubjects = Subjects.Select(s => new SubjectBO()
            {
                SubjectID = s.SubjectID,
                SubjectCode = s.SubjectCode,
                Name = s.Name,
                IsEnable = s.IsEnable

            });

            if (isActive.HasValue)
            {
                allSubjects=allSubjects.Where(s=>s.IsEnable==isActive.Value);
            }
            

            return allSubjects;
        }

        /// <summary>
        /// get ont subject by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SubjectBO GetSubjetByID(long id)
        {
            var subject = Subjects.Select(s => new SubjectBO()
            {
                SubjectID = s.SubjectID,
                SubjectCode = s.SubjectCode,
                Name = s.Name,
                IsEnable = s.IsEnable
            }).Where(s => s.SubjectID == id).FirstOrDefault();
            return subject;
        }

        /// <summary>
        /// Delete Subject by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteSubject(long id, out string msg)
        {

            msg = "";
            var subject = Subjects.SingleOrDefault(s => s.SubjectID == id);

            try
            {

                if (subject != null)
                {
                    bool isSubjectUsed=Teacher_Subject_Allocation.Any(s=>s.SubjectID == subject.SubjectID);
                    if (isSubjectUsed)
                    {
                        msg = "This subject "+subject.Name+ " is in Use.";
                        return false;
                    }
                    Subjects.Remove(subject);
                    SaveChanges();
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
        /// Add or Edit Data
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SaveSubject(SubjectBO subject, out string msg)
        {
            msg = "";

            bool existingSubject = Subjects.Any(s => s.SubjectID == subject.SubjectID);

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
                    var editSubjet = Subjects.SingleOrDefault(s => s.SubjectID == subject.SubjectID);

                    if (editSubjet == null)
                    {
                        msg = "Unable to find the subject for edit";
                        return false;
                    }

                    isSubCodeAvailable=Subjects.Any(s=>s.SubjectCode== subject.SubjectCode && s.SubjectID!=subject.SubjectID);
                    isSubNameAvailable = Subjects.Any(s => s.Name == subject.Name && s.SubjectID != subject.SubjectID);


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
                    SaveChanges();
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
                var newSubject = new Subject();
                newSubject.SubjectCode = subject.SubjectCode;
                newSubject.Name = subject.Name;
                newSubject.IsEnable = subject.IsEnable;
                Subjects.Add(newSubject);
                SaveChanges();
                msg = "Subject Added Successfully!";
                return true;
            }
            catch (Exception error)
            {
                msg = error.Message;
                return false;
            }

        }

        public bool CheckSubjectInUse(long id)
        {
            bool SubjectInUse = Teacher_Subject_Allocation.Any(a => a.SubjectID == id);
            if (SubjectInUse)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// To check this subject name already in used or not when we adding nw data or edit data
        /// </summary>
        /// <param name="subjectName"></param>
        /// <returns></returns>
        public bool CheckSubjectName(string subjectName)
        {
            
            bool existingSubjectName = Subjects.Any(s => s.Name == subjectName);
            if (existingSubjectName)
            {
                
                return false;
            }
            return true;
        }

        /// <summary>
        /// To check this subject code already in used or not when we add the data or edit data`
        /// </summary>
        /// <param name="subjectCode"></param>
        /// <returns></returns>
        public bool CheckSubjectCode(string subjectCode)
        {
           
            bool existingSubjectCode = Subjects.Any(s => s.SubjectCode == subjectCode);
            if ( existingSubjectCode)
            {
                return false;
            }
            return true;
           
        }

        /// <summary>
        /// To access subject enable disable button
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <param name="msg"></param>
        /// <returns></returns>

        public bool ToggleEnable(long id,bool isEnable, out string msg)
        {
            var subject = Subjects.SingleOrDefault(s => s.SubjectID == id);
            if (subject != null)
            {
                if (isEnable)
                {
                    subject.IsEnable= true;
                    SaveChanges();
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
                        SaveChanges();
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

        /// <summary>
        /// To access Search bar
        /// </summary>
        /// <param name="item"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<SubjectBO>GetSearchSubjects(string item,string criteria)
        {
            var allCriteria = GetAllSubject();

            if(criteria == "SubjectCode") 
            {
                allCriteria=allCriteria.Where(s => s.SubjectCode.ToUpper().Contains(item.ToUpper())).ToList();
            }
            else if(criteria == "Name")
            {
                allCriteria=allCriteria.Where(s=>s.Name.ToUpper().Contains(item.ToUpper())).ToList();
            }
            else
            {
                allCriteria=allCriteria.Where(s=>s.SubjectCode.ToUpper().Contains(item.ToUpper())|| s.Name.ToUpper().Contains(item.ToUpper())).ToList();
            }
            return allCriteria;
        }







    }
}
