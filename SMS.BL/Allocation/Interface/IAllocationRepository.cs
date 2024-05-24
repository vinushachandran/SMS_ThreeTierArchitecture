/// <summary>
/// This class represents the student allocation and subject allocation interface
/// </summary>
/// <author>Vinusha</author>

using SMS.Models.Allocation;
using SMS.ViewModels.Allocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Allocation.Interface
{
    public interface IAllocationRepository
    {
        /// <summary>
        /// get all the subject allocaton details
        /// </summary>
        /// <returns></returns>
        IEnumerable<SubjectAllocationGroupByTeacherViewModel> GetAllSubjectAllocation();

        /// <summary>
        /// Get one subject allocation details by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SubjectAllocationBO GetSubjetAllocationByID(long id);

        /// <summary>
        /// Check this subject allocation allocated for any students
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckSubjectAllocationInUse(long id);

        /// <summary>
        /// Delete one subject allocation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool DeleteSubjectAllocation(long id, out string msg);

        /// <summary>
        /// Check this subject allocation already allocated (duplicated)
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>
        bool CheckDuplicateSubjectAllocation(SubjectAllocationBO subjectAllocation);

        /// <summary>
        /// Add new subject allocation or edit the subject allocation
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool SaveSubjectAllocation(SubjectAllocationBO subjectAllocation, out string msg);

        /// <summary>
        /// Seaching the subject allocation
        /// </summary>
        /// <param name="item"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<SubjectAllocationGroupByTeacherViewModel> GetSearchSubjectAllocations(string item, string criteria);

        /// <summary>
        /// Display all the Student allocations
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<StudentAllocationGroupByStudentViewModel> GetAllStudentAllocation(bool? isActive = null);

        /// <summary>
        /// Get one student allocation by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StudentAllocationBO GetStudentAllocationByID(long id);

        /// <summary>
        /// Delete one student allocation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool DeleteStudentAllocation(long id, out string msg);

        /// <summary>
        /// Delete the all allocation for one student
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool DeleteAllStudentAllocations(long studentId, out string msg);

        /// <summary>
        /// Check the duplicate entres
        /// </summary>
        /// <param name="studentAllocation"></param>
        /// <returns></returns>
        bool CheckDuplicateStudentAllocation(StudentAllocationBO studentAllocation);

        /// <summary>
        /// Add a new student allocation 
        /// </summary>
        /// <param name="studentAllocation"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool SaveStudentAllocation(StudentAllocationBO studentAllocation, out string msg);

        /// <summary>
        /// Get the details what are the subject allocated
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> GetAllocatedSubjects();

        /// <summary>
        /// to get the data who are the tachers allocated for one subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<object> GetAllocatedTeachers(long id);

        /// <summary>
        /// Get get the subject allocation id for finding subject id and teacher id
        /// </summary>
        /// <param name="subjectID"></param>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        long GetSubjectAllocationID(long subjectID, long teacherID);

        /// <summary>
        /// Seaching student allocation details
        /// </summary>
        /// <param name="item"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<StudentAllocationGroupByStudentViewModel> GetSearchStudentAllocations(string item, string criteria);

    }
}
