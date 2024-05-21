/// <summary>
///
/// </summary>
/// <author>Vinusha</author>

using SMS.BL;
using SMS.BL.Subject;
using SMS.BL.Subject.Interface;
using SMS.Data;
using SMS.Models.Subject;
using SMS.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class SubjectController : Controller
    {
        //private readonly SubjectBL _subjectBL = new SubjectBL();

        private readonly ISubjectRepository _subjectRepository;
        // GET: Subject

        public SubjectController() 
        { 
            _subjectRepository=new SubjectRepository(new SMS_DBEntities());
        }
        public ActionResult Index()
        {

            return View();
        }


        /// <summary>
        /// To dispaly all data
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult All(int pageNumber, int pageSize, bool? isActive = null)
        {
            var allSubject = new SubjectViewModels();
            //allSubject.Subjects = _subjectBL.GetAllSubject(isActive);
            allSubject.Subjects = _subjectRepository.GetAllSubject(isActive);
            List<SubjectBO> pageData;
            int totalPages;
            Pagination(pageNumber, pageSize, allSubject, out pageData, out totalPages);

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
        /// For the pagination 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="allSubject"></param>
        /// <param name="pageData"></param>
        /// <param name="totalPages"></param>

        private static void Pagination(int pageNumber, int pageSize, SubjectViewModels allSubject, out List<SubjectBO> pageData, out int totalPages)
        {
            int skip = (pageNumber - 1) * pageSize;

            pageData = allSubject.Subjects.OrderBy(s => s.SubjectID).Skip(skip).Take(pageSize).ToList();
            int totalRecords = allSubject.Subjects.Count();
            totalPages = totalRecords / pageSize;
        }

        /// <summary>
        /// Deleting data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var msg = "";
            
            try
            {
                // bool isDelete = _subjectBL.DeleteSubject(id, out msg);
                bool isDelete = _subjectRepository.DeleteSubject(id, out msg);


                return Json(new { success = isDelete, message = msg });
            }
            catch 
            {
                return Json(new { success = false, message = msg });
            }
        }

        /// <summary>
        /// To create a new subject recoard or edit exising recoard by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Add(long id=0)
        {
            if (id == 0)
            {
                return PartialView("_Add",new SubjectBO());

            }
            else
            {

                //var exsitingSubject=_subjectBL.GetSubjetByID(id);
                var exsitingSubject = _subjectRepository.GetSubjetByID(id);
                return PartialView("_Add", exsitingSubject);
            }
           
        }
        /// <summary>
        /// for add a new data
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SubjectBO subject)
        {
            var msg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    // bool isSaveSuccess = _subjectBL.SaveSubject(subject, out msg);
                    bool isSaveSuccess = _subjectRepository.SaveSubject(subject, out msg);

                    return Json(new { success = isSaveSuccess, message = msg });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error occurred while adding the subject: " + ex.Message });
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


        
        public JsonResult IsSubCodeAvailable(string subCode)
        {
            // bool isAvailable = _subjectBL.CheckSubjectCode(subCode);
            bool isAvailable = _subjectRepository.CheckSubjectCode(subCode);
            return Json(isAvailable, JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult IsSubNameAvailable(string subName)
        {
            //bool isAvailable = _subjectBL.CheckSubjectName(subName);
            bool isAvailable = _subjectRepository.CheckSubjectName(subName);
            return Json(isAvailable, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ToggleSubject(int id,bool enable)
        {
            try
            {
                // bool isToggle = _subjectBL.ToggleEnable(id, enable, out string msg);
                bool isToggle = _subjectRepository.ToggleEnable(id, enable, out string msg);

                return Json(new { success = isToggle, message =msg });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet]
        public ActionResult Search(string query, string criteria)
        {
            //var searchResults = new SubjectViewModels();

            // var searchResults = _subjectBL.GetSearchSubjects(query, criteria).ToList();
            var searchResults = _subjectRepository.GetSearchSubjects(query, criteria).ToList();


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