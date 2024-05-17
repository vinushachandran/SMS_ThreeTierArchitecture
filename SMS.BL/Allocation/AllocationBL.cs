using SMS.BL.Student;
using SMS.Data;
using SMS.Models.Allocation;
using SMS.Models.Student;
using SMS.Models.Subject;
using SMS.ViewModels.Allocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Allocation
{
    public class AllocationBL : SMS_DBEntities
    {
        //*******************************************For Subject Allocation***************************************************************

        /// <summary>
        /// get all subject allocation details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubjectAllocationGroupByTeacherViewModel> GetAllSubjectAllocation()
        {

            var allSubjectAllocations = Teacher_Subject_Allocation.Include("Subject").Include("Teacher").ToList();           

            if (allSubjectAllocations.Count > 0)
            {
                var result = allSubjectAllocations.Select(item => new
                {
                    SubjectAllocationID = item.SubjectAllocationID,
                    SubjectCode = item.Subject.SubjectCode,
                    SubjectName = item.Subject.Name,
                    TeacherRegNo = item.Teacher.TeacherRegNo,
                    TeacherName = item.Teacher.DisplayName
                }).GroupBy(s => new {s.TeacherName,s.TeacherRegNo}).ToList();

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

        public SubjectAllocationBO GetSubjetAllocationByID(long id)
        {
            var subjectAllocation = Teacher_Subject_Allocation.Select(s => new SubjectAllocationBO()
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
            bool subjectAllocationInUse = Student_Subject_Teacher_Allocation.Any(a => a.SubjectAllocationID == id);
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
            var subjectAllocation = Teacher_Subject_Allocation.SingleOrDefault(s => s.SubjectAllocationID == id);
            

            try
            {

                if (subjectAllocation != null)
                {
                    if (CheckSubjectAllocationInUse(id))
                    {
                        msg = "This subject allocation is allocated for student.";
                        return false;
                    }
                    Teacher_Subject_Allocation.Remove(subjectAllocation);
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
        /// Check one teacher already allocated for same subject
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>
        public bool CheckDuplicateSubjectAllocation(SubjectAllocationBO subjectAllocation)
        {
            bool isDuplicateAllocation = Teacher_Subject_Allocation.Any(s => s.TeacherID == subjectAllocation.TeacherID && s.SubjectID == subjectAllocation.SubjectID);
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

            bool exitingSubjectAllocation = Teacher_Subject_Allocation.Any(s => s.SubjectAllocationID == subjectAllocation.SubjectAllocationID);
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
                    var editSubjectAllocation = Teacher_Subject_Allocation.SingleOrDefault(s => s.SubjectAllocationID == subjectAllocation.SubjectAllocationID);
                    
                    if (editSubjectAllocation == null)
                    {
                        msg = "Unable to find the subject allocation for edit";
                        return false;
                    }

                    editSubjectAllocation.SubjectID = subjectAllocation.SubjectID;
                    SaveChanges();
                    msg = "Subject Allocation Updated Successfully!";
                    return true;

                }
                

                var newSubjectAllocation = new Teacher_Subject_Allocation();
                newSubjectAllocation.TeacherID = subjectAllocation.TeacherID; 
                newSubjectAllocation.SubjectID = subjectAllocation.SubjectID;
                
                Teacher_Subject_Allocation.Add(newSubjectAllocation);
                SaveChanges();
                msg = "Subject allocation Added Successfully!";
                return true;
            }
            catch (Exception error)
            {
                msg = error.Message;
                return false;
            }

        }



        //******************************************************For Student Allocation********************************************************


        /// <summary>
        /// To get all the students allocation details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StudentAllocationGroupByStudentViewModel> GetAllStudentAllocation()
        {
            var allStudentAllocations = Student_Subject_Teacher_Allocation
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
                    TeacherName = item.Teacher_Subject_Allocation.Teacher.DisplayName
                }).GroupBy(s => new {s.StudentName,s.studentRegNo}).ToList();

                var data = result.Select(s => new StudentAllocationGroupByStudentViewModel
                {
                    StudentName = s.Key.StudentName,
                    StudentRegNo = s.Key.studentRegNo,
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
            var studentAllocation = Student_Subject_Teacher_Allocation.Select(s => new StudentAllocationBO()
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
            var studentAllocation = Student_Subject_Teacher_Allocation.SingleOrDefault(s => s.StudentAllocationID == id);


            try
            {

                if (studentAllocation != null)
                {
                    Student_Subject_Teacher_Allocation.Remove(studentAllocation);
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
        /// To check this allocation already exist or not
        /// </summary>
        /// <param name="studentAllocation"></param>
        /// <returns></returns>
        public bool CheckDuplicateStudentAllocation(StudentAllocationBO studentAllocation)
        {
            bool isDuplicateAllocation = Student_Subject_Teacher_Allocation.Any(s => s.SubjectAllocationID == studentAllocation.SubjectAllocationID && s.StudentID == studentAllocation.StudentID);
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

            bool exitingStudentAllocation = Student_Subject_Teacher_Allocation.Any(s => s.StudentAllocationID == studentAllocation.StudentAllocationID);
            

            try
            {
                if (CheckDuplicateStudentAllocation(studentAllocation))
                {
                    msg = "This Allocation already exist!";
                    return false;
                }
                if (exitingStudentAllocation)
                {
                  var editStudentAllocation = Student_Subject_Teacher_Allocation.SingleOrDefault(s => s.StudentAllocationID == studentAllocation.StudentAllocationID);

                    if (editStudentAllocation == null)
                    {
                        msg = "Unable to find the student allocation for edit";
                        return false;
                    }

                    editStudentAllocation.StudentAllocationID = studentAllocation.StudentAllocationID;
                    SaveChanges();
                    msg = "Student Allocation Updated Successfully!";
                    return true;

                }
                
                var newStudentAllocation = new Student_Subject_Teacher_Allocation();
                newStudentAllocation.SubjectAllocationID = studentAllocation.SubjectAllocationID;
                newStudentAllocation.StudentID = studentAllocation.StudentID;

                Student_Subject_Teacher_Allocation.Add(newStudentAllocation);
                SaveChanges();
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
            var subjects=Teacher_Subject_Allocation.Select(a=>new {SubjectID=a.SubjectID,Name=a.Subject.SubjectCode+" - "+a.Subject.Name}).Distinct();
            return subjects;

        }

        /// <summary>
        /// Get the Teacher details who allocated in teacher subject allocation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<object> GetAllocatedTeachers(long id)
        {
            var teachers = Teacher_Subject_Allocation. Where(t=> t.SubjectID==id).Select(a => new { Value = a.TeacherID, Text = a.Teacher.DisplayName}).ToList();
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
            var allocationID=Teacher_Subject_Allocation.Where(s=>s.SubjectID==subjectID && s.TeacherID==teacherID).Select(s=>s.SubjectAllocationID).FirstOrDefault();
            return allocationID;
        }




    }
}
