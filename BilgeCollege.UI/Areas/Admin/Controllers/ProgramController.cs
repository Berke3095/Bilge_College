using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.MODELS.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProgramController : Controller
    {
        private readonly I_ClassroomServiceManager _classroomServiceManager;
        private readonly I_Classrooms_AltTopicsManager _classrooms_AltTopicsManager;
        private readonly I_AltTopicServiceManager _altTopicServiceManager;

        public ProgramController(I_ClassroomServiceManager classroomServiceManager, I_Classrooms_AltTopicsManager classrooms_AltTopicsManager, I_AltTopicServiceManager altTopicServiceManager)
        {
            _classroomServiceManager = classroomServiceManager;
            _classrooms_AltTopicsManager = classrooms_AltTopicsManager;
            _altTopicServiceManager = altTopicServiceManager;
        }

        public IActionResult Show(int? id)
        {
            if(id != null)
            {
                var classroom = _classroomServiceManager.GetById((int)id);

                ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
                return View(classroom);
            }
            else
            {
                var classroom_altTopic = _classrooms_AltTopicsManager.GetAll().Where(x => x.ClassroomId == id);
                var altTopics = new List<AltTopic>();

                foreach(var item in classroom_altTopic)
                {
                    altTopics.Add(_altTopicServiceManager.GetById((int)item.AltTopicId));
                }
                ViewBag.AltTopics = altTopics;
                ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
                return View();
            }
            
        }
    }
}
