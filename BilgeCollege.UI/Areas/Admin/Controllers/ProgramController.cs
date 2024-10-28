using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.UI.Areas.Admin.Views.Models;
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
        private readonly I_DayScheduleServiceManager _dayScheduleServiceManager;

        public ProgramController(I_ClassroomServiceManager classroomServiceManager, I_AltTopicServiceManager altTopicServiceManager, I_DayScheduleServiceManager dayScheduleServiceManager)
        {
            _classroomServiceManager = classroomServiceManager;
            _altTopicServiceManager = altTopicServiceManager;
            _dayScheduleServiceManager = dayScheduleServiceManager;
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
                var daySchedules = _dayScheduleServiceManager.GetAllActives().Where(x => x.ClassroomId == classroom.Id).ToList();

                ViewBag.ActiveAltTopics = _altTopicServiceManager.GetAllActives();
                ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();

                ViewBag.Days = ScheduleManager.GetDays();
                return View(daySchedules);
            }
        }

        public IActionResult Create(int id)
        {
            var daySchedule = _dayScheduleServiceManager.GetById(id);

            DayScheduleVM dayScheduleVM = new DayScheduleVM
            {
                Id = id,
                DaySchedule = daySchedule,
            };

            ViewBag.ActiveAltTopics = _altTopicServiceManager.GetAllActives();
            return View(dayScheduleVM);
        }

        [HttpPost]
        public IActionResult Create(DayScheduleVM dayScheduleVM)
        {
            if(dayScheduleVM != null)
            {
                var daySchedule = _dayScheduleServiceManager.GetById(dayScheduleVM.Id);
                daySchedule.AltTopics = new List<AltTopic>();

                int i = 0;
                while(i < 8)
                {
                    daySchedule.AltTopics.Add(dayScheduleVM.AltTopics[i]);
                    i++;
                }

                var classroom = _classroomServiceManager.GetById((int)daySchedule.ClassroomId);
                return RedirectToAction("Show", "Program", new {id = classroom.Id});
            }

            return View();
        }
    }
}
