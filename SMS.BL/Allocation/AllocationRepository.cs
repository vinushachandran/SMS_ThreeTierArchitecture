using SMS.BL.Allocation.Interface;
using SMS.Data;
using SMS.Models.Allocation;
using SMS.ViewModels.Allocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Allocation
{
    public class AllocationRepository : IAllocationRepository
    {

        private readonly SMS_DBEntities _dbEntities;

        public AllocationRepository(SMS_DBEntities DBEntities)
        {
            _dbEntities = DBEntities;
        }
        //*******************************************For Subject Allocation***************************************************************

        /// <summary>
        /// get all subject allocation details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubjectAllocationGroupByTeacherViewModel> GetAllSubjectAllocation()
        {

            var allSubjectAllocations = _dbEntities.Teacher_Subject_Allocation.Include("Subject").Include("Teacher").ToList();

            if (allSubjectAllocations.Count > 0)
            {
                var result = allSubjectAllocations.Select(item => new
                {
                    SubjectAllocationID = item.SubjectAllocationID,
                    SubjectCode = item.Subject.SubjectCode,
                    SubjectName = item.Subject.Name,
                    TeacherRegNo = item.Teacher.TeacherRegNo,
                    TeacherName = item.Teacher.DisplayName
                }).GroupBy(s => new { s.TeacherName, s.TeacherRegNo }).ToList();

                var data = result.Select(s => new SubjectAllocationGroupByTeacherViewModel
                {
                    TeacherName = s.Key.TeacherName,
                    TeacherRegNo = s.Key.TeacherRegNo,
                    SubjectAllocations = s.Select(x => new SubjectAllocationViewModel
                    {
                        SubjectAllocationID = x.SubjectAllocationID,
                        SubjectCode = x.SubjectCode,
                        SubjectName = x.SubjectName,

                    }).ToList()
                });

                return data;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get one subject allocation using that id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public SubjectAllocationBO GetSubjetAllocationByID(long id)
        {
            var subjectAllocation = _dbEntities.Teacher_Subject_Allocation.Select(s => new SubjectAllocationBO()
            {
                SubjectAllocationID = s.SubjectAllocationID,
                SubjectID = s.SubjectID,
                TeacherID = s.TeacherID,
            }).Where(s => s.SubjectAllocationID == id).FirstOrDefault();
            return subjectAllocation;
        }


        /// <summary>
        /// Check this subject allocation allocated for any student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckSubjectAllocationInUse(long id)
        {
            bool subjectAllocationInUse = _dbEntities.Student_Subject_Teacher_Allocation.Any(a => a.SubjectAllocationID == id);
            return subjectAllocationInUse;

        }

        /// <summary>
        /// Delete A Subhect allocation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteSubjectAllocation(long id, out string msg)
        {

            msg = "";
            var subjectAllocation = _dbEntities.Teacher_Subject_Allocation.SingleOrDefault(s => s.SubjectAllocationID == id);


            try
            {

                if (subjectAllocation != null)
                {
                    if (CheckSubjectAllocationInUse(id))
                    {
                        msg = "This subject allocation is allocated for student.";
                        return false;
                    }
                    _dbEntities.Teacher_Subject_Allocation.Remove(subjectAllocation);
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
        /// Check one teacher already allocated for same subject
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>
        public bool CheckDuplicateSubjectAllocation(SubjectAllocationBO subjectAllocation)
        {
            bool isDuplicateAllocation = _dbEntities.Teacher_Subject_Allocation.Any(s => s.TeacherID == subjectAllocation.TeacherID && s.SubjectID == subjectAllocation.SubjectID);
            return isDuplicateAllocation;
        }

        /// <summary>
        /// Create or edit a allocation
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SaveSubjectAllocation(SubjectAllocationBO subjectAllocation, out string msg)
        {
            msg = "";

            bool exitingSubjectAllocation = _dbEntities.Teacher_Subject_Allocation.Any(s => s.SubjectAllocationID == subjectAllocation.SubjectAllocationID);
            bool SubjectAllocationInUse = CheckSubjectAllocationInUse(subjectAllocation.SubjectAllocationID);

            try
            {
                if (CheckDuplicateSubjectAllocation(subjectAllocation))
                {
                    msg = "This Allocation already exist!";
                    return false;
                }
                if (exitingSubjectAllocation)
                {

                    if (SubjectAllocationInUse)
                    {
                        msg = "The Subject allocation is Followed by a sudent";
                        return false;
                    }
                    var editSubjectAllocation = _dbEntities.Teacher_Subject_Allocation.SingleOrDefault(s => s.SubjectAllocationID == subjectAllocation.SubjectAllocationID);

                    if (editSubjectAllocation == null)
                    {
                        msg = "Unable to find the subject allocation for edit";
                        return false;
                    }

                    editSubjectAllocation.SubjectID = subjectAllocation.SubjectID;
                    _dbEntities.SaveChanges();
                    msg = "Subject Allocation Updated Successfully!";
                    return true;

                }


                var newSubjectAllocation = new Teacher_Subject_Allocation();
                newSubjectAllocation.TeacherID = subjectAllocation.TeacherID;
                newSubjectAllocation.SubjectID = subjectAllocation.SubjectID;

                _dbEntities.Teacher_Subject_Allocation.Add(newSubjectAllocation);
                _dbEntities.SaveChanges();
                msg = "Subject allocation Added Successfully!";
                return true;
            }
            catch (Exception error)
            {
                msg = error.Message;
                return false;
            }

        }


        public IEnumerable<SubjectAllocationGroupByTeacherViewModel> GetSearchSubjectAllocations(string item, string criteria)
        {
            var allCriteria = GetAllSubjectAllocation();


            if (criteria == "Teacher-name1")
            {
                allCriteria = allCriteria.Where(s => s.TeacherName.ToUpper().Contains(item.ToUpper())).ToList();
            }
            else if (criteria == "Subject-name1")
            {
                allCriteria = allCriteria.Where(s => s.SubjectAllocations.Any(t => t.SubjectName.ToUpper().Contains(item.ToUpper()))).ToList();
            }
            else
            {
                allCriteria = allCriteria.Where(s => s.TeacherName.ToUpper().Contains(item.ToUpper()) || s.SubjectAllocations.Any(t => t.SubjectName.ToUpper().Contains(item.ToUpper()))).ToList();
            }
            return allCriteria;
        }



        //******************************************************For Student Allocation********************************************************


        /// <summary>
        /// To get all the students allocation details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StudentAllocationGroupByStudentViewModel> GetAllStudentAllocation(bool? isActive = null)
        {
            var allStudentAllocations = _dbEntities.Student_Subject_Teacher_Allocation
                          .Include("Teacher_Subject_Allocation.Subject")
                          .Include("Teacher_Subject_Allocation.Teacher")
                          .Include("Student").ToList();


            if (allStudentAllocations.Count > 0)
            {
                var result = allStudentAllocations.Select(item => new
                {
                    studentAllocationID = item.StudentAllocationID,
                    StudentID = item.StudentID,
                    StudentName = item.Student.DisplayName,
                    studentRegNo = item.Student.StudentRegNo,
                    SubjectID = item.Teacher_Subject_Allocation.SubjectID,
                    subjectCode = item.Teacher_Subject_Allocation.Subject.SubjectCode,
                    SubjectName = item.Teacher_Subject_Allocation.Subject.Name,
                    TeacherID = item.Teacher_Subject_Allocation.TeacherID,
                    teacherRegNo = item.Teacher_Subject_Allocation.Teacher.TeacherRegNo,
                    TeacherName = item.Teacher_Subject_Allocation.Teacher.DisplayName,
                    IsEnable = item.Student.IsEnable,
                }).GroupBy(s => new { s.StudentName, s.studentRegNo, s.StudentID, s.IsEnable }).ToList();

                var data = result.Select(s => new StudentAllocationGroupByStudentViewModel
                {
                    StudentName = s.Key.StudentName,
                    StudentRegNo = s.Key.studentRegNo,
                    StudentID = s.Key.StudentID,
                    isEnable = s.Key.IsEnable,
                    subjectAllocations = s.GroupBy(x => new { x.TeacherName, x.teacherRegNo })
                        .Select(y => new SubjectAllocationGroupByTeacherViewModel
                        {
                            TeacherName = y.Key.TeacherName,
                            TeacherRegNo = y.Key.teacherRegNo,
                            SubjectAllocations = y.Select(subject => new SubjectAllocationViewModel
                            {
                                StudentAllocationID = subject.studentAllocationID,
                                SubjectCode = subject.subjectCode,
                                SubjectName = subject.SubjectName,
                                TeacherRegNo = subject.teacherRegNo
                            }).ToList()
                        }).ToList()
                });
                if (isActive.HasValue)
                {
                    data = data.Where(s => s.isEnable == isActive.Value);
                }
                return data;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Get one student allocation by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentAllocationBO GetStudentAllocationByID(long id)
        {
            var studentAllocation = _dbEntities.Student_Subject_Teacher_Allocation.Select(s => new StudentAllocationBO()
            {
                StudentAllocationID = s.StudentAllocationID,
                SubjectAllocationID = s.SubjectAllocationID,
                StudentID = s.StudentID,
            }).Where(s => s.StudentAllocationID == id).FirstOrDefault();
            return studentAllocation;
        }


        /// <summary>
        /// Delete one student allocation by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteStudentAllocation(long id, out string msg)
        {

            msg = "";
            var studentAllocation = _dbEntities.Student_Subject_Teacher_Allocation.SingleOrDefault(s => s.StudentAllocationID == id);


            try
            {

                if (studentAllocation != null)
                {
                    _dbEntities.Student_Subject_Teacher_Allocation.Remove(studentAllocation);
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
        /// Remove the full allocations of that student
        /// </summary>
        /// <param name="studentRegNo"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteAllStudentAllocations(long studentId, out string msg)
        {
            msg = "";
            try
            {
                var studentAllocations = _dbEntities.Student_Subject_Teacher_Allocation.Where(s => s.StudentID == studentId).ToList();

                if (studentAllocations != null)
                {
                    _dbEntities.Student_Subject_Teacher_Allocation.RemoveRange(studentAllocations);
                    _dbEntities.SaveChanges();
                    return true;
                }
                msg = "No allocations found for the student.";
                return false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }





        /// <summary>
        /// To check this allocation already exist or not
        /// </summary>
        /// <param name="studentAllocation"></param>
        /// <returns></returns>
        public bool CheckDuplicateStudentAllocation(StudentAllocationBO studentAllocation)
        {
            bool isDuplicateAllocation = _dbEntities.Student_Subject_Teacher_Allocation.Any(s => s.SubjectAllocationID == studentAllocation.SubjectAllocationID && s.StudentID == studentAllocation.StudentID);
            return isDuplicateAllocation;
        }

        /// <summary>
        /// add a new student allocation or edit already exist allocation
        /// </summary>
        /// <param name="studentAllocation"></param>
        /// <param name="msg"></param>
        /// <returns></returns>

        public bool SaveStudentAllocation(StudentAllocationBO studentAllocation, out string msg)
        {
            msg = "";

            bool exitingStudentAllocation = _dbEntities.Student_Subject_Teacher_Allocation.Any(s => s.StudentAllocationID == studentAllocation.StudentAllocationID);


            try
            {
                if (studentAllocation.SubjectAllocationID == 0)
                {
                    msg = "Fill all the fields to create allocation";
                    return false;
                }
                if (CheckDuplicateStudentAllocation(studentAllocation))
                {
                    msg = "This Allocation already exist!";
                    return false;
                }
                if (exitingStudentAllocation)
                {
                    var editStudentAllocation = _dbEntities.Student_Subject_Teacher_Allocation.SingleOrDefault(s => s.StudentAllocationID == studentAllocation.StudentAllocationID);

                    if (editStudentAllocation == null)
                    {
                        msg = "Unable to find the student allocation for edit";
                        return false;
                    }

                    editStudentAllocation.StudentAllocationID = studentAllocation.StudentAllocationID;
                    _dbEntities.SaveChanges();
                    msg = "Student Allocation Updated Successfully!";
                    return true;

                }

                var newStudentAllocation = new Student_Subject_Teacher_Allocation();
                newStudentAllocation.SubjectAllocationID = studentAllocation.SubjectAllocationID;
                newStudentAllocation.StudentID = studentAllocation.StudentID;

                _dbEntities.Student_Subject_Teacher_Allocation.Add(newStudentAllocation);
                _dbEntities.SaveChanges();
                msg = "Student allocation Added Successfully!";
                return true;
            }
            catch (Exception error)
            {
                msg = error.Message;
                return false;
            }

        }

        /// <summary>
        /// Get the subject details which allocated in teacher subject allocation
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetAllocatedSubjects()
        {
            var subjects = _dbEntities.Teacher_Subject_Allocation.Select(a => new { SubjectID = a.SubjectID, Name = a.Subject.SubjectCode + " - " + a.Subject.Name }).Distinct();
            return subjects;

        }

        /// <summary>
        /// Get the Teacher details who allocated in teacher subject allocation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<object> GetAllocatedTeachers(long id)
        {
            var teachers = _dbEntities.Teacher_Subject_Allocation.Where(t => t.SubjectID == id).Select(a => new { Value = a.SubjectAllocationID, Text = a.Teacher.DisplayName + " - " + a.Teacher.TeacherRegNo }).ToList();
            return teachers;

        }

        /// <summary>
        /// Get the subject allocation id when techer id and subject id are same
        /// </summary>
        /// <param name="subjectID"></param>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        public long GetSubjectAllocationID(long subjectID, long teacherID)
        {
            var allocationID = _dbEntities.Teacher_Subject_Allocation.Where(s => s.SubjectID == subjectID && s.TeacherID == teacherID).Select(s => s.SubjectAllocationID).FirstOrDefault();
            return allocationID;
        }


        public IEnumerable<StudentAllocationGroupByStudentViewModel> GetSearchStudentAllocations(string item, string criteria)
        {
            var allCriteria = GetAllStudentAllocation();

            if (criteria == "Student-name")
            {
                allCriteria = allCriteria.Where(s => s.StudentName.ToUpper().Contains(item.ToUpper())).ToList();
            }
            else if (criteria == "Teacher-name")
            {
                allCriteria = allCriteria.Where(s => s.subjectAllocations.Any(t => t.TeacherName.ToUpper().Contains(item.ToUpper()))).ToList();
            }
            else if (criteria == "Subject-name")
            {
                allCriteria = allCriteria.Where(s => s.subjectAllocations.Any(t => t.SubjectAllocations.Any(x => x.SubjectName.ToUpper().Contains(item.ToUpper())))).ToList();
            }
            else
            {
                allCriteria = allCriteria.Where(s => s.StudentName.ToUpper().Contains(item.ToUpper()) || s.subjectAllocations.Any(t => t.TeacherName.ToUpper().Contains(item.ToUpper())) || s.subjectAllocations.Any(t => t.SubjectAllocations.Any(x => x.SubjectName.ToUpper().Contains(item.ToUpper())))).ToList();
            }
            return allCriteria;
        }
    }
}
