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
        private readonly I_ClassHourServiceManager _classHourServiceManager;

        public ProgramController(I_ClassroomServiceManager classroomServiceManager, I_AltTopicServiceManager altTopicServiceManager, I_DayScheduleServiceManager dayScheduleServiceManager, I_ClassHourServiceManager classHourServiceManager)
        {
            _classroomServiceManager = classroomServiceManager;
            _altTopicServiceManager = altTopicServiceManager;
            _dayScheduleServiceManager = dayScheduleServiceManager;
            _classHourServiceManager = classHourServiceManager;
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
                ViewBag.ClassHours = _classHourServiceManager.GetAll();

                ViewBag.Days = ScheduleManager.GetDays();
                return View(daySchedules);
            }
        }

        public IActionResult Update(int id)
        {
            var daySchedule = _dayScheduleServiceManager.GetById(id);

            DayScheduleVM dayScheduleVM = new DayScheduleVM
            {
                Id = id,
                Day = daySchedule.Day,
            };

            ViewBag.ActiveAltTopics = _altTopicServiceManager.GetAllActives();
            ViewBag.ClassHours = _classHourServiceManager.GetAll().Where(x => x.DayScheduleId == daySchedule.Id).ToList();
            return View(dayScheduleVM);
        }

        [HttpPost]
        public IActionResult Update(DayScheduleVM dayScheduleVM)
        {
            if(dayScheduleVM != null)
            {
                var daySchedule = _dayScheduleServiceManager.GetById(dayScheduleVM.Id);
                var classHours = _classHourServiceManager.GetAll().Where(x => x.DayScheduleId == daySchedule.Id).ToList();

                for(int i = 0; i < 8; i++) // 8 class hours
                {

                    classHours[i].AltTopicId = dayScheduleVM.AltTopicIds[i];
                }

                _dayScheduleServiceManager.Update(daySchedule);

                var classroom = _classroomServiceManager.GetById((int)daySchedule.ClassroomId);
                return RedirectToAction("Show", "Program", new { id = classroom.Id });

            }
            return View(); // Exception
        }
    }
}
