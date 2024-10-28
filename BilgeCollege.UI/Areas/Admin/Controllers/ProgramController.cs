using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Utils;
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
        private readonly I_AltTopicServiceManager _altTopicServiceManager;

        public ProgramController(I_ClassroomServiceManager classroomServiceManager, I_AltTopicServiceManager altTopicServiceManager)
        {
            _classroomServiceManager = classroomServiceManager;
            _altTopicServiceManager = altTopicServiceManager;
        }

        public IActionResult Show(int? id)
        {
            if(id == null)
            {
                ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
                ViewBag.Days = ScheduleManager.GetDays();
                return View();
            }
            else
            {
                var classroom = _classroomServiceManager.GetById((int)id);

                ViewBag.ActiveAltTopics = _altTopicServiceManager.GetAllActives();
                ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();

                ViewBag.Days = ScheduleManager.GetDays();
                return View(classroom.DaySchedules);
            }
            
        }
    }
}
