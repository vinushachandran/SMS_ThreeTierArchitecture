using SMS.BL;
using SMS.BL.Allocation;
using SMS.BL.Student;
using SMS.BL.Subject.Interface;
using SMS.BL.Subject;
using SMS.BL.Teacher;
using SMS.Data;
using SMS.Models.Allocation;
using SMS.Models.Teacher;
using SMS.ViewModels.Allocation;
using SMS.ViewModels.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMS.BL.Allocation.Interface;
using SMS.BL.Teacher.Interface;
using SMS.BL.Student.Interface;

namespace SMS.Controllers
{
    
    public class AllocationController : Controller
    {
        private readonly IAllocationRepository _allocationRepository;

        // GET: Subject

       

        private readonly ITeacherRepository _teacherRepository;

        // GET: Subject

        

        private readonly ISubjectRepository _subjectRepository;

        private readonly IStudentRepository _studentRepository;
        
        public AllocationController() 
        {
            _allocationRepository = new AllocationRepository(new SMS_DBEntities());
            _studentRepository = new StudentRepository(new SMS_DBEntities());
            _subjectRepository = new SubjectRepository(new SMS_DBEntities());
            _teacherRepository=new TeacherRepository(new SMS_DBEntities());
            ViewBag.Subjects = _subjectRepository.GetAllSubject().Where(s => s.IsEnable == true).Select(s => new { SubjectID = s.SubjectID, Name = s.SubjectCode + " - " + s.Name }).ToList();
            ViewBag.Teachers = _teacherRepository.GetAllTeacher().Where(t => t.IsEnable == true).Select(t => new { TeacherID = t.TeacherID, DisplayName = t.TeacherRegNo + " -  " + t.DisplayName }).ToList();
            ViewBag.Students = _studentRepository.GetAllStudents().Where(t => t.IsEnable == true).Select(t => new { StudentID = t.StudentID, DisplayName = t.StudentRegNo + " -  " + t.DisplayName }).ToList();
        }
        
        public ActionResult Index()
        {
            return View();
        }

        //****************************************************************For Subject Allocation****************************************************************************
        /// <summary>
        /// Get all allocation details
        /// </summary>
        /// <returns></returns>
        public ActionResult AllSubjectTeacherAllocation()
        {
           var allocatedSubject=new AllocationViewModel();

            allocatedSubject.SubjectAllocations = _allocationRepository.GetAllSubjectAllocation();
            return PartialView("_AllSubjectAllocations",allocatedSubject.SubjectAllocations);

        }

        /// <summary>
        /// Delete one allocation based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteTeacherSubjectAllocation(long id)
        {
            var msg = "";
            try
            {

                bool isDelete = _allocationRepository.DeleteSubjectAllocation(id, out msg);

                return Json(new { success = isDelete, message = msg });
            }
            catch
            {
                return Json(new { success = false, message = msg });
            }
        }

        /// <summary>
        /// For add allocation or edit allocation by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddTeacherSubjectAllocation(long id = 0)
        {
            if (id == 0)
            {
                return PartialView("_AddSubjectAllocation", new SubjectAllocationBO());

            }
            else
            {

                var exsitingAllocation = _allocationRepository.GetSubjetAllocationByID(id);
                return PartialView("_AddSubjectAllocation", exsitingAllocation);
            }

        }

        /// <summary>
        /// Add a new allocation
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTeacherSubjectAllocation(SubjectAllocationBO subjectAllocation)
        {
            var msg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSaveSuccess = _allocationRepository.SaveSubjectAllocation(subjectAllocation, out msg);

                    return Json(new { success = isSaveSuccess, message = msg });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error occurred while adding the Subject allocation: " + ex.Message });
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

        [HttpGet]
        public ActionResult SearchSubjectAllocation(string query, string criteria)
        {

            var searchResults = _allocationRepository.GetSearchSubjectAllocations(query, criteria).ToList();
            //return Json(searchResults, JsonRequestBehavior.AllowGet);

            if (searchResults.Count > 0)
            {
                return PartialView("_SearchSubjectAllocationResults", searchResults);
            }
            else
            {
                return PartialView("_SearchSubjectAllocationResults", null);
            }
        }


        //*********************************************For Student allocation******************************************************
        public ActionResult AllStudentAllocation(bool? isActive = null)
        {
            var allocatedStudents = new AllocationViewModel();
            allocatedStudents.StudentAllocations = _allocationRepository.GetAllStudentAllocation(isActive);
            return PartialView("__AllStudentAllocations", allocatedStudents.StudentAllocations);


            //var allAllocationStudents = _allocationBL.GetAllStudentAllocation();

            //if (allAllocationStudents != null)
            //{

            //    return Json(new { success = true, data = allAllocationStudents }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { success = false, message = "Data Not Found" }, JsonRequestBehavior.AllowGet);
            //}

        }

        /// <summary>
        /// remove one allocation of a student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteStudentAllocation(long id)
        {
            var msg = "";
            try
            {

                bool isDelete = _allocationRepository.DeleteStudentAllocation(id, out msg);

                return Json(new { success = isDelete, message = msg });
            }
            catch
            {
                return Json(new { success = false, message = msg });
            }
        }

        /// <summary>
        /// remove all allocations of a student
        /// </summary>
        /// <param name="studentRegNo"></param>
        /// <returns></returns>
        public ActionResult DeleteAllStudentAllocations(long id)
        {
            var msg = "";
            try
            {
                bool isDelete = _allocationRepository.DeleteAllStudentAllocations(id, out msg);
                return Json(new { success = isDelete, message = msg });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        /// <summary>
        /// Add new allocation or edit existing allocation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddStudentAllocation(long id = 0)
        {
            if (id == 0)
            {
                return PartialView("_AddStudentAllocation", new StudentAllocationBO());

            }
            else
            {

                var exsitingAllocation = _allocationRepository.GetStudentAllocationByID(id);
                return PartialView("_AddStudentAllocation", exsitingAllocation);
            }

        }

        /// <summary>
        /// add a new allocation or edit allocation
        /// </summary>
        /// <param name="studentAllocation"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudentAllocation(StudentAllocationBO studentAllocation)
        {
            var msg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSaveSuccess = _allocationRepository.SaveStudentAllocation(studentAllocation, out msg);

                    return Json(new { success = isSaveSuccess, message = msg });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error occurred while adding the Student allocation: " + ex.Message });
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
        /// Get subject details which is allocated to a teacher
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllocatedSubject()
        {
            var data=_allocationRepository.GetAllocatedSubjects().ToList();

            if (data.Count>0)
            {

                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Data Not Found" }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// Get teacher details who is allocated to a teacher
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public JsonResult GetTeachersBySubject(long subjectId)
        {
            var selectedTeachers = _allocationRepository.GetAllocatedTeachers(subjectId);
            return Json(selectedTeachers, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// get the subject allocation id when particular theacher allocated for a particular subject
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllocationID(long subjectId, long teacherId)
        {
            var selectedAllocationID = _allocationRepository.GetSubjectAllocationID(subjectId,teacherId);
            return Json(selectedAllocationID, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult SearchStudentAllocation(string query, string criteria)
        {

            var searchResults = _allocationRepository.GetSearchStudentAllocations(query, criteria).ToList();
            //return Json(searchResults, JsonRequestBehavior.AllowGet);

            if (searchResults.Count > 0)
            {
                return PartialView("_SearchStudentAllocationResults", searchResults);
            }
            else
            {
                return PartialView("_SearchStudentAllocationResults", null);
            }
        }

    }
}