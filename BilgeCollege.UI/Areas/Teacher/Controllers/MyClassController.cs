using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BilgeCollege.UI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class MyClassController : Controller
    {
        private readonly I_ClassroomServiceManager _classroomServiceManager;
        private readonly I_AltTopicServiceManager _altTopicServiceManager;
        private readonly I_TeacherServiceManager _teacherServiceManager;
        private readonly I_StudentServiceManager _studentServiceManager;
        private readonly I_GradeServiceManager _gradeServiceManager;
        private readonly UserManager<User> _userManager;

        public MyClassController(I_ClassroomServiceManager classroomServiceManager, I_AltTopicServiceManager altTopicServiceManager, I_TeacherServiceManager teacherServiceManager, I_StudentServiceManager studentServiceManager, I_GradeServiceManager gradeServiceManager, UserManager<User> userManager)
        {
            _classroomServiceManager = classroomServiceManager;
            _altTopicServiceManager = altTopicServiceManager;
            _teacherServiceManager = teacherServiceManager;
            _studentServiceManager = studentServiceManager;
            _gradeServiceManager = gradeServiceManager;
            _userManager = userManager;
        }

        public IActionResult Show(int? classroomId, int? altTopicId)
        {
            var userId = _userManager.GetUserId(User);
            var thisTeacher = _teacherServiceManager.GetAllActives().First(x => x.UserId == userId);
            var altTopics = _altTopicServiceManager.GetAllActives().Where(x => x.TeacherId == thisTeacher.Id).ToList();

            var classrooms = new List<Classroom>();
            foreach (var altTopic in altTopics)
            {
                var classroomToAdd = _classroomServiceManager.GetDbSet().Include(x => x.AltTopics).Where(x => x.AltTopics.Contains(altTopic) && x.State == MODELS.Enums.StateEnum.Active).ToList();

                foreach (var item in classroomToAdd)
                {
                    if(!classrooms.Contains(item))
                    {
                        classrooms.Add(item);
                    }
                }
            }

            ViewBag.Classrooms = classrooms;
            
            if(classroomId != null)
            {
                var classroom = _classroomServiceManager.GetDbSet().Include(x => x.AltTopics).First(x => x.Id == classroomId);
                ViewBag.AltTopics = classroom.AltTopics.Where(x => x.TeacherId == thisTeacher.Id).ToList();

                ViewBag.ClassroomId = classroom.Id;

                if (altTopicId != null)
                {
                    var grades = _gradeServiceManager.GetDbSet().Include(x => x.Student).Where(x => x.AltTopicId == altTopicId).ToList();
                    return View(grades);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Show(Grade grade, int gradeId)
        {
            if(grade != null)
            {
                var originalGrade = _gradeServiceManager.GetById(gradeId);
                originalGrade.MidTermGrade = grade.MidTermGrade;
                originalGrade.FinalGrade = grade.FinalGrade;
                originalGrade.PerformanceGrade = grade.PerformanceGrade;

                _gradeServiceManager.Update(originalGrade);

                var altTopic = _altTopicServiceManager.GetById((int)originalGrade.AltTopicId);
                var student = _studentServiceManager.GetById((int)originalGrade.StudentId);

                return RedirectToAction("Show", "MyClass", new { classroomId = student.ClassroomId, altTopicId = altTopic.Id });
            }

            return View();
        }
    }
}
