/// <summary>
///
/// </summary>
/// <author>Vinusha</author>

using SMS.BL.Student;
using SMS.BL.Subject.Interface;
using SMS.BL.Subject;
using SMS.BL.Teacher;
using SMS.Data;
using SMS.Models.Student;
using SMS.Models.Teacher;
using SMS.ViewModels.Student;
using SMS.ViewModels.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMS.BL.Student.Interface;




namespace SMS.Controllers
{

    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        // GET: Subject

        public StudentController()
        {
            _studentRepository = new StudentRepository(new SMS_DBEntities());
        }

        //private readonly StudentBL _studentBL = new StudentBL();
        
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// To get all the student in display
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult All(int pageNumber, int pageSize, bool? isActive = null)
        {
            var allStudent = new StudentViewModel();
            allStudent.Students = _studentRepository.GetAllStudents(isActive);
            List<StudentBO> pageData;
            int totalPages;
            Pagination(pageNumber, pageSize, allStudent, out pageData, out totalPages);

            if (pageData.Count > 0)
            {
                return Json(new { success = true, data = pageData, totalPages = totalPages }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "No Data Found", totalPages = totalPages }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// To control the pagination
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="allTeacher"></param>
        /// <param name="pageData"></param>
        /// <param name="totalPages"></param>

        private static void Pagination(int pageNumber, int pageSize, StudentViewModel allStudent, out List<StudentBO> pageData, out int totalPages)
        {
            int skip = (pageNumber - 1) * pageSize;

            pageData = allStudent.Students.OrderBy(s => s.StudentID).Skip(skip).Take(pageSize).ToList();
            int totalRecords = allStudent.Students.Count();
            totalPages = totalRecords / pageSize;
        }

        /// <summary>
        /// To delete one data based on the studentID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var msg = "";

            try
            {
                bool isDelete = _studentRepository.DeleteStudent(id, out msg);


                return Json(new { success = isDelete, message = msg });
            }
            catch
            {
                return Json(new { success = false, message = msg });
            }
        }

        /// <summary>
        /// To access enable disable button
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enable"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult ToggleStudent(int id, bool enable)
        {
            try
            {
                bool isToggle = _studentRepository.ToggleEnable(id, enable, out string msg);

                return Json(new { success = isToggle, message = msg });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        /// <summary>
        /// To add or edit one data 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Add(long id = 0)
        {
            if (id == 0)
            {
                return PartialView("_Add", new StudentBO());

            }
            else
            {

                var exsitingStudent = _studentRepository.GetStudentByID(id);
                return PartialView("_Add", exsitingStudent);
            }

        }

        /// <summary>
        /// To add a new data
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(StudentBO student)
        {
            var msg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSaveSuccess = _studentRepository.SaveStudent(student, out msg);

                    return Json(new { success = isSaveSuccess, message = msg });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error occurred while adding the student: " + ex.Message });
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return Json(new { success = false, message = "Please fill all details.", errors = errors });
            }

        }


        /// <summary>
        /// To check the student registration number already exist or not
        /// </summary>
        /// <param name="regNo"></param>
        /// <returns></returns>
        public JsonResult IsStudentRegAvailable(string regNo)
        {
            bool isAvailable = _studentRepository.CheckTeacherRegNo(regNo);
            return Json(isAvailable, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// To check this student displayname already available or not
        /// </summary>
        /// <param name="studentName"></param>
        /// <returns></returns>

        public JsonResult IsStudentNameAvailable(string studentName)
        {
            bool isAvailable = _studentRepository.CheckStudentName(studentName);
            return Json(isAvailable, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// To check one email address already exist or not
        /// </summary>
        /// <param name="teacherEmail"></param>
        /// <returns></returns>
        public JsonResult IsStudentEmailAvailable(string studentEmail)
        {
            bool isAvailable = _studentRepository.CheckStudentEmail(studentEmail);
            return Json(isAvailable, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// To access the seach bar
        /// </summary>
        /// <param name="query"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Search(string query, string criteria)
        {

            var searchResults = _studentRepository.GetSearchStudents(query, criteria).ToList();


            if (searchResults.Count > 0)
            {
                return PartialView("_SearchResults", searchResults);
            }
            else
            {
                return PartialView("_SearchResults", null);
            }
        }
    }
}