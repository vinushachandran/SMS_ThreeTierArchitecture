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
        IEnumerable<SubjectAllocationGroupByTeacherViewModel> GetAllSubjectAllocation();

        SubjectAllocationBO GetSubjetAllocationByID(long id);
        bool CheckSubjectAllocationInUse(long id);
        bool DeleteSubjectAllocation(long id, out string msg);
        bool CheckDuplicateSubjectAllocation(SubjectAllocationBO subjectAllocation);
        bool SaveSubjectAllocation(SubjectAllocationBO subjectAllocation, out string msg);
        IEnumerable<SubjectAllocationGroupByTeacherViewModel> GetSearchSubjectAllocations(string item, string criteria);
        IEnumerable<StudentAllocationGroupByStudentViewModel> GetAllStudentAllocation(bool? isActive = null);
        StudentAllocationBO GetStudentAllocationByID(long id);
        bool DeleteStudentAllocation(long id, out string msg);
        bool DeleteAllStudentAllocations(long studentId, out string msg);
        bool CheckDuplicateStudentAllocation(StudentAllocationBO studentAllocation);
        bool SaveStudentAllocation(StudentAllocationBO studentAllocation, out string msg);
        IEnumerable<object> GetAllocatedSubjects();
        IEnumerable<object> GetAllocatedTeachers(long id);
        long GetSubjectAllocationID(long subjectID, long teacherID);
        IEnumerable<StudentAllocationGroupByStudentViewModel> GetSearchStudentAllocations(string item, string criteria);

    }
}
