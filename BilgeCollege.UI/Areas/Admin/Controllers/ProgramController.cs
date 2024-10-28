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
            if (id == null)
            {
                ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
                ViewBag.Days = ScheduleManager.GetDays();
                return View();
            }
            else
            {
                var classroom = _classroomServiceManager.GetById((int)id);
                var daySchedules = _dayScheduleServiceManager.GetAll().Where(x => x.ClassroomId == classroom.Id).ToList();

                List<ClassHour> classHours = new List<ClassHour>();

                foreach(var daySchedule in daySchedules)
                {
                    var classHoursForItem = _classHourServiceManager.GetAll().Where(x => x.DayScheduleId == daySchedule.Id).ToList();
                    
                    int i = 0;
                    foreach(var classHour in classHoursForItem)
                    {
                        daySchedule.ClassHours[i] = classHour;
                        i++;
                    }
                }

                _altTopicServiceManager.GetAllActives();
                ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();

                ViewData["Class"] = classroom.Grade;

                ViewBag.Days = ScheduleManager.GetDays();
                return View(daySchedules);
            }
        }

        public IActionResult Update(int id)
        {
            var daySchedule = _dayScheduleServiceManager.GetById(id);
            var classHours = _classHourServiceManager.GetAll().Where(x => x.DayScheduleId == id).ToList();

            DayScheduleVM dayScheduleVM = new DayScheduleVM
            {
                Id = id,
                Day = daySchedule.Day
            };

            for (int i = 0; i < 8; i++)
            {
                dayScheduleVM.AltTopicIds[i] = (int)classHours[i].AltTopicId;
            }

            ViewBag.ActiveAltTopics = _altTopicServiceManager.GetAllActives();
            _classHourServiceManager.GetAll().Where(x => x.DayScheduleId == daySchedule.Id).ToList();
            return View(dayScheduleVM);
        }

        [HttpPost]
        public IActionResult Update(DayScheduleVM dayScheduleVM)
        {
            if (dayScheduleVM != null)
            {
                var daySchedule = _dayScheduleServiceManager.GetById(dayScheduleVM.Id);
                var classHours = _classHourServiceManager.GetAll().Where(x => x.DayScheduleId == daySchedule.Id).ToList();

                for (int i = 0; i < 8; i++) // 8 class hours
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
