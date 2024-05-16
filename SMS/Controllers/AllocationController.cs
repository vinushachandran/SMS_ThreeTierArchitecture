using SMS.BL;
using SMS.BL.Allocation;
using SMS.BL.Student;
using SMS.BL.Teacher;
using SMS.Models.Allocation;
using SMS.Models.Teacher;
using SMS.ViewModels.Allocation;
using SMS.ViewModels.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    
    public class AllocationController : Controller
    {
        private readonly AllocationBL _allocationBL = new AllocationBL();
        private readonly TeacherBL _teacherBL=new TeacherBL();
        private readonly SubjectBL _subjectBL = new SubjectBL();
        private readonly StudentBL _studentBL = new StudentBL();
        public AllocationController() 
        {
            ViewBag.Subjects = _subjectBL.GetAllSubject().Where(s => s.IsEnable == true).Select(s => new { SubjectID = s.SubjectID, Name = s.SubjectCode + " - " + s.Name }).ToList();
            ViewBag.Teachers = _teacherBL.GetAllTeacher().Where(t => t.IsEnable == true).Select(t => new { TeacherID = t.TeacherID, DisplayName = t.TeacherRegNo + " -  " + t.DisplayName }).ToList();
            ViewBag.Students = _studentBL.GetAllStudents().Where(t => t.IsEnable == true).Select(t => new { StudentID = t.StudentID, DisplayName = t.StudentRegNo + " -  " + t.DisplayName }).ToList();
        }
        // GET: Allocation
        public ActionResult Index()
        {
            return View();
        }

        //For Subject Allocation

        public ActionResult AllSubjectTeacherAllocation()
        {
            var allAllocationSubjects = _allocationBL.GetAllSubjectAllocation();

            if (allAllocationSubjects !=null)
            {

                return Json(new { success = true, data = allAllocationSubjects }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Data Not Found" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DeleteTeacherSubjectAllocation(long id)
        {
            var msg = "";
            try
            {

                bool isDelete = _allocationBL.DeleteSubjectAllocation(id, out msg);

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

                var exsitingAllocation = _allocationBL.GetSubjetAllocationByID(id);
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
                    bool isSaveSuccess = _allocationBL.SaveSubjectAllocation(subjectAllocation, out msg);

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


        //For Student allocation
        public ActionResult AllStudentAllocation()
        {
            var allAllocationStudents = _allocationBL.GetAllStudentAllocation();

            if (allAllocationStudents != null)
            {

                return Json(new { success = true, data = allAllocationStudents }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Data Not Found" }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult DeleteStudentAllocation(long id)
        {
            var msg = "";
            try
            {

                bool isDelete = _allocationBL.DeleteStudentAllocation(id, out msg);

                return Json(new { success = isDelete, message = msg });
            }
            catch
            {
                return Json(new { success = false, message = msg });
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

                var exsitingAllocation = _allocationBL.GetStudentAllocationByID(id);
                return PartialView("_AddStudentAllocation", exsitingAllocation);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudentAllocation(StudentAllocationBO studentAllocation)
        {
            var msg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSaveSuccess = _allocationBL.SaveStudentAllocation(studentAllocation, out msg);

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


        public ActionResult GetAllocatedSubject()
        {
            var data=_allocationBL.GetAllocatedSubjects().ToList();

            if (data.Count>0)
            {

                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Data Not Found" }, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult GetTeachersBySubject(long subjectId)
        {
            var selectedTeachers = _allocationBL.GetAllocatedTeachers(subjectId);
            return Json(selectedTeachers, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetAllocationID(long subjectId, long teacherId)
        {
            var selectedAllocationID = _allocationBL.GetSubjectAllocationID(subjectId,teacherId);
            return Json(selectedAllocationID, JsonRequestBehavior.AllowGet);

        }



    }
}