using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BilgeCollege.UI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class MyClassController : Controller
    {
        private readonly I_ClassroomServiceManager _classroomServiceManager;
        private readonly I_AltTopicServiceManager _altTopicServiceManager;
        private readonly I_TeacherServiceManager _teacherServiceManager;

        public MyClassController(I_ClassroomServiceManager classroomServiceManager, I_AltTopicServiceManager altTopicServiceManager, I_TeacherServiceManager teacherServiceManager)
        {
            _classroomServiceManager = classroomServiceManager;
            _altTopicServiceManager = altTopicServiceManager;
            _teacherServiceManager = teacherServiceManager;
        }

        public IActionResult Show(int? id)
        {
            var thisTeacher = _teacherServiceManager.GetByGuidId(AccountController.AccountId);
            var altTopics = _altTopicServiceManager.GetAllActives().Where(x => x.TeacherId == thisTeacher.Id).ToList();
            var classrooms = new List<Classroom>();
            foreach (var altTopic in altTopics)
            {
                var classroomToAdd = _classroomServiceManager.GetAllActives().Where(x => x.AltTopics.Contains(altTopic)).ToList();
                classrooms.AddRange(classroomToAdd);
            }

            ViewBag.Classrooms = classrooms;
            
            if(id != null)
            {
                var classroom = _classroomServiceManager.GetById((int)id);
                return View(classroom);
            }
            return View();
        }
    }
}
