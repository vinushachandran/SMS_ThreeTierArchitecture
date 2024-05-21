/// <summary>
///
/// </summary>
/// <author>Vinusha</author>

using SMS.BL;
using SMS.BL.Teacher;
using SMS.BL.Teacher.Interface;
using SMS.Data;
using SMS.Models.Subject;
using SMS.Models.Teacher;
using SMS.ViewModels.Subject;
using SMS.ViewModels.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class TeacherController : Controller
    {
        //private readonly TeacherBL _teacherBL = new TeacherBL();

        private readonly ITeacherRepository _teacherRepository;

        public TeacherController()
        {
            _teacherRepository=new TeacherRepository(new SMS_DBEntities());

        }
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// To display All the data 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult All(int pageNumber, int pageSize, bool? isActive = null)
        {
            var allTeacher = new TeacherViewModel();
            allTeacher.Teachers = _teacherRepository.GetAllTeacher(isActive);
            List<TeacherBO> pageData;
            int totalPages;
            Pagination(pageNumber, pageSize, allTeacher, out pageData, out totalPages);

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
        /// To access pagination
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="allTeacher"></param>
        /// <param name="pageData"></param>
        /// <param name="totalPages"></param>
        private static void Pagination(int pageNumber, int pageSize, TeacherViewModel allTeacher, out List<TeacherBO> pageData, out int totalPages)
        {
            int skip = (pageNumber - 1) * pageSize;

            pageData = allTeacher.Teachers.OrderBy(s => s.TeacherID).Skip(skip).Take(pageSize).ToList();
            int totalRecords = allTeacher.Teachers.Count();
            totalPages = totalRecords / pageSize;
        }

        /// <summary>
        /// To delete one data based on the teacherID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var msg = "";

            try
            {
                bool isDelete = _teacherRepository.DeleteTeacher(id, out msg);


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
        public ActionResult ToggleTeacher(int id, bool enable)
        {
            try
            {
                bool isToggle = _teacherRepository.ToggleEnable(id, enable, out string msg);

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
                return PartialView("_Add", new TeacherBO());

            }
            else
            {

                var exsitingTeacher = _teacherRepository.GetTeacherByID(id);
                return PartialView("_Add", exsitingTeacher);
            }

        }

        /// <summary>
        /// To add a new data
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TeacherBO teacher)
        {
            var msg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSaveSuccess = _teacherRepository.SaveTeacher(teacher, out msg);

                    return Json(new { success = isSaveSuccess, message = msg });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error occurred while adding the teacher: " + ex.Message });
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
        /// To check the teacher registration number already exist or not
        /// </summary>
        /// <param name="regNo"></param>
        /// <returns></returns>
        public JsonResult IsTeacherRegAvailable(string regNo)
        {
            bool isAvailable = _teacherRepository.CheckTeacherRegNo(regNo);
            return Json(isAvailable, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// To check this teacher displayname already available or not
        /// </summary>
        /// <param name="teacherName"></param>
        /// <returns></returns>

        public JsonResult IsTeacherNameAvailable(string teacherName)
        {
            bool isAvailable = _teacherRepository.CheckTeacherName(teacherName);
            return Json(isAvailable, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// To check one email address already exist or not
        /// </summary>
        /// <param name="teacherEmail"></param>
        /// <returns></returns>
        public JsonResult IsTeacherEmailAvailable(string teacherEmail)
        {
            bool isAvailable = _teacherRepository.CheckTeacherEmail(teacherEmail);
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

            var searchResults = _teacherRepository.GetSearchTeachers(query, criteria).ToList();


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