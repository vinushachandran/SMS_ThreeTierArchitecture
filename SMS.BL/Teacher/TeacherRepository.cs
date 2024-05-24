/// <summary>
/// This class represents the teacher repository
/// </summary>
/// <author>Vinusha</author>

using SMS.BL.Teacher.Interface;
using SMS.Data;
using SMS.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Teacher
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SMS_DBEntities _dbEntities;

        public TeacherRepository(SMS_DBEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }
        /// <summary>
        /// To get all the Teacher details
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<TeacherBO> GetAllTeacher(bool? isActive = null)
        {
            var allTeacher = _dbEntities.Teachers.Select(s => new TeacherBO()
            {
                TeacherID = s.TeacherID,
                TeacherRegNo = s.TeacherRegNo,
                FirstName = s.FirstName,
                MiddleName = s.MiddleName,
                LastName = s.LastName,
                DisplayName = s.DisplayName,
                Email = s.Email,
                Gender = s.Gender,
                DOB = s.DOB,
                Address = s.Address,
                ContactNo = s.ContactNo,
                IsEnable = s.IsEnable

            });

            if (isActive.HasValue)
            {
                allTeacher = allTeacher.Where(s => s.IsEnable == isActive.Value);
            }


            return allTeacher;
        }

        /// <summary>
        /// To get one teacher detail by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TeacherBO GetTeacherByID(long id)
        {
            var teacher = _dbEntities.Teachers.Select(s => new TeacherBO()
            {
                TeacherID = s.TeacherID,
                TeacherRegNo = s.TeacherRegNo,
                FirstName = s.FirstName,
                MiddleName = s.MiddleName,
                LastName = s.LastName,
                DisplayName = s.DisplayName,
                Email = s.Email,
                Gender = s.Gender,
                DOB = s.DOB,
                Address = s.Address,
                ContactNo = s.ContactNo,
                IsEnable = s.IsEnable
            }).Where(s => s.TeacherID == id).FirstOrDefault();
            return teacher;
        }

        /// <summary>
        /// To delete one teacher detail bt the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteTeacher(long id, out string msg)
        {

            msg = "";
            var teacher = _dbEntities.Teachers.SingleOrDefault(s => s.TeacherID == id);

            try
            {

                if (teacher != null)
                {
                    bool isSubjectUsed = _dbEntities.Teacher_Subject_Allocation.Any(s => s.TeacherID == teacher.TeacherID);
                    if (isSubjectUsed)
                    {
                        msg = "This teacher " + teacher.DisplayName + " is allocated for subject.";
                        return false;
                    }
                    _dbEntities.Teachers.Remove(teacher);
                    _dbEntities.SaveChanges();
                    return true;
                }
                msg = "Already removed this teacher";
                return false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

        }
        /// <summary>
        /// To check this teacher name already exits
        /// </summary>
        /// <param name="teacherName"></param>
        /// <returns></returns>
        public bool CheckTeacherName(string teacherName)
        {

            bool existingTeacherName = _dbEntities.Teachers.Any(s => s.DisplayName == teacherName);
            if (existingTeacherName)
            {

                return false;
            }
            return true;
        }

        /// <summary>
        /// Check this teacher reg number already exist
        /// </summary>
        /// <param name="TeacherRegNumber"></param>
        /// <returns></returns>
        public bool CheckTeacherRegNo(string teacherRegNumber)
        {

            bool existingTeacherRegNo = _dbEntities.Teachers.Any(s => s.TeacherRegNo == teacherRegNumber);
            if (existingTeacherRegNo)
            {
                return false;
            }
            return true;

        }
        /// <summary>
        /// To chek this email address already exist
        /// </summary>
        /// <param name="teacherEmail"></param>
        /// <returns></returns>
        public bool CheckTeacherEmail(string teacherEmail)
        {

            bool existingTeacherEmail = _dbEntities.Teachers.Any(s => s.Email == teacherEmail);
            if (existingTeacherEmail)
            {
                return false;
            }
            return true;

        }

        /// <summary>
        /// To check this teacher allready in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckTeacherInUse(long id)
        {
            bool TeacherInUse = _dbEntities.Teacher_Subject_Allocation.Any(a => a.TeacherID == id);
            return TeacherInUse;

        }
        /// <summary>
        /// To access enable button and check validations
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ToggleEnable(long id, bool isEnable, out string msg)
        {
            var teacher = _dbEntities.Teachers.SingleOrDefault(s => s.TeacherID == id);
            if (teacher != null)
            {
                if (isEnable)
                {
                    teacher.IsEnable = true;
                    _dbEntities.SaveChanges();
                    msg = "This teacher " + teacher.DisplayName + " successfully enabled! ";
                    return true;
                }
                else
                {
                    if (CheckTeacherInUse(id))
                    {
                        msg = "This teacher " + teacher.DisplayName + " Allocated for a subject you can't disable it!";
                        return false;
                    }
                    else
                    {
                        teacher.IsEnable = false;
                        _dbEntities.SaveChanges();
                        msg = "This teacher " + teacher.DisplayName + " successfully disabled! ";
                        return true;
                    }
                }
            }
            else
            {
                msg = "Teacher not found !";
                return false;
            }
        }

        /// <summary>
        /// Add a new teacher or edit a teacher data based on id
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SaveTeacher(TeacherBO teacher, out string msg)
        {
            msg = "";

            bool existingTeacher = _dbEntities.Teachers.Any(s => s.TeacherID == teacher.TeacherID);

            bool teacherInUse = CheckTeacherInUse(teacher.TeacherID);
            bool isTeacherRegAvailable = CheckTeacherRegNo(teacher.TeacherRegNo);
            bool isTeacherNameAvailable = CheckTeacherName(teacher.DisplayName);
            bool isTeacherEmailAvailable = CheckTeacherEmail(teacher.Email);
            try
            {
                if (existingTeacher)
                {
                    if (teacherInUse)
                    {
                        msg = "The teacher " + teacher.DisplayName + " allocated for a subject";
                        return false;
                    }
                    var editTeacher = _dbEntities.Teachers.SingleOrDefault(s => s.TeacherID == teacher.TeacherID);

                    if (editTeacher == null)
                    {
                        msg = "Unable to find the subject for edit";
                        return false;
                    }

                    isTeacherRegAvailable = _dbEntities.Teachers.Any(s => s.TeacherRegNo == teacher.TeacherRegNo && s.TeacherID != teacher.TeacherID);
                    isTeacherNameAvailable = _dbEntities.Teachers.Any(s => s.DisplayName == teacher.DisplayName && s.TeacherID != teacher.TeacherID);
                    isTeacherEmailAvailable = _dbEntities.Teachers.Any(s => s.Email == teacher.Email && s.TeacherID != teacher.TeacherID);


                    if (isTeacherRegAvailable)
                    {
                        msg = "Teacher registration number already exists.";
                        return false;
                    }

                    if (isTeacherNameAvailable)
                    {
                        msg = "Teacher display name already exists.";
                        return false;
                    }

                    if (isTeacherEmailAvailable)
                    {
                        msg = "Teacher email address already exists.";
                        return false;
                    }

                    editTeacher.TeacherRegNo = teacher.TeacherRegNo;
                    editTeacher.FirstName = teacher.FirstName;
                    editTeacher.MiddleName = teacher.MiddleName;
                    editTeacher.LastName = teacher.LastName;
                    editTeacher.DisplayName = teacher.DisplayName;
                    editTeacher.Email = teacher.Email;
                    editTeacher.Gender = teacher.Gender;
                    editTeacher.DOB = teacher.DOB;
                    editTeacher.Address = teacher.Address;
                    editTeacher.ContactNo = teacher.ContactNo;
                    editTeacher.IsEnable = teacher.IsEnable;
                    _dbEntities.SaveChanges();
                    msg = "Teacher Updated Successfully!";
                    return true;

                }
                if (!isTeacherRegAvailable)
                {
                    msg = "Teacher registration number already exists.";
                    return false;
                }

                if (!isTeacherNameAvailable)
                {
                    msg = "Teacher display name already exists.";
                    return false;
                }

                if (!isTeacherEmailAvailable)
                {
                    msg = "Teacher email address already exists.";
                    return false;
                }
                var newTeacher = new SMS.Data.Teacher();
                newTeacher.TeacherRegNo = teacher.TeacherRegNo;
                newTeacher.FirstName = teacher.FirstName;
                newTeacher.MiddleName = teacher.MiddleName;
                newTeacher.LastName = teacher.LastName;
                newTeacher.DisplayName = teacher.DisplayName;
                newTeacher.Email = teacher.Email;
                newTeacher.Gender = teacher.Gender;
                newTeacher.DOB = teacher.DOB;
                newTeacher.Address = teacher.Address;
                newTeacher.ContactNo = teacher.ContactNo;
                newTeacher.IsEnable = teacher.IsEnable;
                _dbEntities.Teachers.Add(newTeacher);
                _dbEntities.SaveChanges();
                msg = "Teacher Added Successfully!";
                return true;
            }
            catch (Exception error)
            {
                msg = error.Message;
                return false;
            }

        }

        /// <summary>
        /// To access search bar based on search criteria
        /// </summary>
        /// <param name="item"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<TeacherBO> GetSearchTeachers(string item, string criteria)
        {
            var allCriteria = GetAllTeacher();

            if (criteria == "TeacherReg")
            {
                allCriteria = allCriteria.Where(s => s.TeacherRegNo.ToUpper().Contains(item.ToUpper())).ToList();
            }
            else if (criteria == "DisplayName")
            {
                allCriteria = allCriteria.Where(s => s.DisplayName.ToUpper().Contains(item.ToUpper())).ToList();
            }
            else
            {
                allCriteria = allCriteria.Where(s => s.TeacherRegNo.ToUpper().Contains(item.ToUpper()) || s.DisplayName.ToUpper().Contains(item.ToUpper())).ToList();
            }
            return allCriteria;
        }
    }
}
