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
        private readonly I_DaySchedule_AltTopicManager _daySchedule_AltTopicManager;
        private readonly I_AltTopicServiceManager _altTopicServiceManager;
        private readonly I_DayScheduleServiceManager _dayScheduleServiceManager;

        public ProgramController(I_ClassroomServiceManager classroomServiceManager, I_DaySchedule_AltTopicManager daySchedule_AltTopicManager, I_DayScheduleServiceManager dayScheduleServiceManager, I_AltTopicServiceManager altTopicServiceManager)
        {
            _classroomServiceManager = classroomServiceManager;
            _daySchedule_AltTopicManager = daySchedule_AltTopicManager;
            _altTopicServiceManager = altTopicServiceManager;
            _dayScheduleServiceManager = dayScheduleServiceManager;
        }

        public IActionResult Show(int? id)
        {
            if(id == null)
            {
                ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
                return View();
            }
            else
            {
                var classroom = _classroomServiceManager.GetById((int)id);

                ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
                ViewBag.Days = ScheduleManager.GetDays();
                return View(classroom.DaySchedules);
            }
            
        }
    }
}
