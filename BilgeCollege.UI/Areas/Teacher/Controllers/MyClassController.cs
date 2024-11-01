using BilgeCollege.BLL.Services.Abstracts;
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
        private readonly UserManager<User> _userManager;

        public MyClassController(I_ClassroomServiceManager classroomServiceManager, I_AltTopicServiceManager altTopicServiceManager, I_TeacherServiceManager teacherServiceManager, UserManager<User> userManager)
        {
            _classroomServiceManager = classroomServiceManager;
            _altTopicServiceManager = altTopicServiceManager;
            _teacherServiceManager = teacherServiceManager;
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
                classrooms.AddRange(classroomToAdd);
            }

            ViewBag.Classrooms = classrooms;
            
            if(classroomId != null)
            {
                var classroom = _classroomServiceManager.GetDbSet().Include(x => x.AltTopics).First(x => x.Id == classroomId);
                ViewBag.AltTopics = classroom.AltTopics.Where(x => x.TeacherId == thisTeacher.Id);

                if(altTopicId != null)
                {
                    
                }

                return View(classroom);
            }
            return View();
        }
    }
}
