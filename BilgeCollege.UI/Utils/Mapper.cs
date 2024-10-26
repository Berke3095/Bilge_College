using BilgeCollege.MODELS.Concretes;
using BilgeCollege.UI.Areas.Admin.Views.Models;

namespace BilgeCollege.UI.Utils
{
    public static class Mapper
    {
        public static StudentVM StudentToStudentVM(Student student)
        {
            StudentVM studentVM = new StudentVM
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                TCK = student.TCK,
                FinishedSchool = student.FinishedSchool,
                FinalGrade = student.FinalGrade,
                Gender = student.Gender,
                GuardianId = student.GuardianId,
                ClassroomId = student.ClassroomId
            };

            return studentVM;
        }

        public static TeacherVM TeacherToTeacherVM(Teacher teacher)
        {
            TeacherVM teacherVM = new TeacherVM
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MainTopicId = teacher.MainTopicId,
                TCK = teacher.TCK
            };

            return teacherVM;
        }
    }
}
