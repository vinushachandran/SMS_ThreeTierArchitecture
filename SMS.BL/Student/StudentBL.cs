using SMS.Data;
using SMS.Models.Student;
using SMS.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Student
{
    public class StudentBL : SMS_DBEntities
    {
        /// <summary>
        /// To get all studets details
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>

        public IEnumerable<StudentBO> GetAllStudents(bool? isActive = null)
        {
            var allStudent = Students.Select(s => new StudentBO()
            {
                StudentID = s.StudentID,
                StudentRegNo = s.StudentRegNo,
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
                allStudent = allStudent.Where(s => s.IsEnable == isActive.Value);
            }


            return allStudent;
        }

        /// <summary>
        /// Get one student details by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public StudentBO GetStudentByID(long id)
        {
            var student = Students.Select(s => new StudentBO()
            {
                StudentID = s.StudentID,
                StudentRegNo = s.StudentRegNo,
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
            }).Where(s => s.StudentID == id).FirstOrDefault();
            return student;
        }


        /// <summary>
        /// To delete one student detail by the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>

        public bool DeleteStudent(long id, out string msg)
        {

            msg = "";
            var student = Students.SingleOrDefault(s => s.StudentID == id);

            try
            {

                if (student != null)
                {
                    bool isStudentUsed = Student_Subject_Teacher_Allocation.Any(s => s.StudentID == student.StudentID);
                    if (student.IsEnable==true)
                    {
                        if (isStudentUsed)
                        {
                            msg = "This student " + student.DisplayName + " is allocated for subject.";
                            return false;
                        }
                        Students.Remove(student);
                        SaveChanges();
                        return true;


                    }
                    
                    Students.Remove(student);
                    SaveChanges();
                    return true;
                }
                msg = "Already removed this subject";
                return false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// To check this student name already exits
        /// </summary>
        /// <param name="studentname"></param>
        /// <returns></returns>
        public bool CheckStudentName(string studentname)
        {

            bool existingStudentName = Students.Any(s => s.DisplayName == studentname);
            if (existingStudentName)
            {

                return false;
            }
            return true;
        }

        /// <summary>
        /// Check this students reg number already exist
        /// </summary>
        /// <param name="studentRegNumber"></param>
        /// <returns></returns>

        public bool CheckTeacherRegNo(string studentRegNumber)
        {

            bool existingStudentRegNo = Students.Any(s => s.StudentRegNo == studentRegNumber);
            if (existingStudentRegNo)
            {
                return false;
            }
            return true;

        }
        /// <summary>
        /// To chek this email address already exist
        /// </summary>
        /// <param name="studentEmail"></param>
        /// <returns></returns>
        public bool CheckStudentEmail(string studentEmail)
        {

            bool existingStudentEmail = Students.Any(s => s.Email == studentEmail);
            if (existingStudentEmail)
            {
                return false;
            }
            return true;

        }

        // <summary>
        /// To check this teacher allready in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public bool CheckStudentInUse(long id)
        {
            bool StudentInUse = Student_Subject_Teacher_Allocation.Any(a => a.StudentID == id);
            if (StudentInUse)
            {
                return true;
            }
            return false;

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
            var student = Students.SingleOrDefault(s => s.StudentID == id);
            if (student != null)
            {
                if (isEnable)
                {
                    student.IsEnable = true;
                    SaveChanges();
                    msg = "This student " + student.DisplayName + " successfully enabled! ";
                    return true;
                }
                else
                {
                    if (CheckStudentInUse(id))
                    {
                        var allAllocation = Student_Subject_Teacher_Allocation.Where(a => a.StudentID == id).ToList();
                        Student_Subject_Teacher_Allocation.RemoveRange(allAllocation);
                        SaveChanges();                        

                    }
                    

                    student.IsEnable=false;
                    SaveChanges() ;
                    msg = "this student " + student.DisplayName + " successfully disabled!";
                    return true;


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
        public bool SaveStudent(StudentBO student, out string msg)
        {
            msg = "";

            bool existingStudent = Students.Any(s => s.StudentID == student.StudentID);
            
            bool studentInUse = CheckStudentInUse(student.StudentID);
            bool isStudentRegAvailable = CheckTeacherRegNo(student.StudentRegNo);
            bool isStudentNameAvailable = CheckStudentName(student.DisplayName);
            bool isStudentEmailAvailable = CheckStudentEmail(student.Email);
            var editStudent = Students.SingleOrDefault(s => s.StudentID == student.StudentID);
            try
            {
                if (existingStudent)
                {
                    if (studentInUse)
                    {
                        if (student.IsEnable == editStudent.IsEnable)
                        {
                            msg = "The student " + student.DisplayName + " allocated for a subject";
                            return false;
                        }
                        var allAllocation = Student_Subject_Teacher_Allocation.Where(a => a.StudentID == student.StudentID).ToList();
                        Student_Subject_Teacher_Allocation.RemoveRange(allAllocation);
                        SaveChanges();
                        return UpdatedStudentDetails(student, out msg, editStudent);

                    }


                    if (editStudent == null)
                    {
                        msg = "Unable to find the student for edit";
                        return false;
                    }

                    isStudentRegAvailable = Students.Any(s => s.StudentRegNo == student.StudentRegNo && s.StudentID != student.StudentID);
                    isStudentNameAvailable = Students.Any(s => s.DisplayName == student.DisplayName && s.StudentID != student.StudentID);
                    isStudentEmailAvailable = Students.Any(s => s.Email == student.Email && s.StudentID != student.StudentID);


                    if (isStudentRegAvailable)
                    {
                        msg = "Student registration number already exists.";
                        return false;
                    }

                    if (isStudentNameAvailable)
                    {
                        msg = "Student display name already exists.";
                        return false;
                    }

                    if (isStudentEmailAvailable)
                    {
                        msg = "Student email address already exists.";
                        return false;
                    }

                    return UpdatedStudentDetails(student, out msg, editStudent);

                }
                if (!isStudentRegAvailable)
                {
                    msg = "Student registration number already exists.";
                    return false;
                }

                if (!isStudentNameAvailable)
                {
                    msg = "Student display name already exists.";
                    return false;
                }

                if (!isStudentEmailAvailable)
                {
                    msg = "Student email address already exists.";
                    return false;
                }
                var newStudent = new SMS.Data.Student();
                newStudent.StudentRegNo = student.StudentRegNo;
                newStudent.FirstName = student.FirstName;
                newStudent.MiddleName = student.MiddleName;
                newStudent.LastName = student.LastName;
                newStudent.DisplayName = student.DisplayName;
                newStudent.Email = student.Email;
                newStudent.Gender = student.Gender;
                newStudent.DOB = student.DOB;
                newStudent.Address = student.Address;
                newStudent.ContactNo = student.ContactNo;
                newStudent.IsEnable = student.IsEnable;
                Students.Add(newStudent);
                SaveChanges();
                msg = "Student Added Successfully!";
                return true;
            }
            catch (Exception error)
            {
                msg = error.Message;
                return false;
            }

        }

        /// <summary>
        /// Update student details 
        /// </summary>
        /// <param name="student"></param>
        /// <param name="msg"></param>
        /// <param name="editStudent"></param>
        /// <returns></returns>
        private bool UpdatedStudentDetails(StudentBO student, out string msg, Data.Student editStudent)
        {
            editStudent.StudentRegNo = student.StudentRegNo;
            editStudent.FirstName = student.FirstName;
            editStudent.MiddleName = student.MiddleName;
            editStudent.LastName = student.LastName;
            editStudent.DisplayName = student.DisplayName;
            editStudent.Email = student.Email;
            editStudent.Gender = student.Gender;
            editStudent.DOB = student.DOB;
            editStudent.Address = student.Address;
            editStudent.ContactNo = student.ContactNo;
            editStudent.IsEnable = student.IsEnable;
            SaveChanges();
            msg = "Student Updated Successfully!";
            return true;
        }

        /// <summary>
        /// To access search bar based on search criteria
        /// </summary>
        /// <param name="item"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<StudentBO> GetSearchStudents(string item, string criteria)
        {
            var allCriteria = GetAllStudents();

            if (criteria == "StudentReg")
            {
                allCriteria = allCriteria.Where(s => s.StudentRegNo.ToUpper().Contains(item.ToUpper())).ToList();
            }
            else if (criteria == "DisplayName")
            {
                allCriteria = allCriteria.Where(s => s.DisplayName.ToUpper().Contains(item.ToUpper())).ToList();
            }
            else
            {
                allCriteria = allCriteria.Where(s => s.StudentRegNo.ToUpper().Contains(item.ToUpper()) || s.DisplayName.ToUpper().Contains(item.ToUpper())).ToList();
            }
            return allCriteria;
        }

    }
}
