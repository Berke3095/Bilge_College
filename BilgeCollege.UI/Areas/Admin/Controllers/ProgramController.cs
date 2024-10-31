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
        private readonly I_StudentServiceManager _studentServiceManager;
        private readonly I_GradeServiceManager _gradeServiceManager;

        public ProgramController(I_ClassroomServiceManager classroomServiceManager, I_AltTopicServiceManager altTopicServiceManager, I_DayScheduleServiceManager dayScheduleServiceManager, I_ClassHourServiceManager classHourServiceManager, I_StudentServiceManager studentServiceManager, I_GradeServiceManager gradeServiceManager)
        {
            _classroomServiceManager = classroomServiceManager;
            _altTopicServiceManager = altTopicServiceManager;
            _dayScheduleServiceManager = dayScheduleServiceManager;
            _classHourServiceManager = classHourServiceManager;
            _studentServiceManager = studentServiceManager;
            _gradeServiceManager = gradeServiceManager;
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
                ViewBag.ActiveStudents = _studentServiceManager.GetAllActives().Where(x => x.ClassroomId == classroom.Id).ToList();

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
                _altTopicServiceManager.GetAllActives();
                _classHourServiceManager.GetAll().Where(x => x.DayScheduleId == dayScheduleVM.Id).ToList();

                var daySchedule = _dayScheduleServiceManager.GetById(dayScheduleVM.Id);
                var classHours = _classHourServiceManager.GetAll().Where(x => x.DayScheduleId == daySchedule.Id).ToList();
                var currentAltTopics = _classroomServiceManager.GetAllAltTopics((int)daySchedule.ClassroomId, _dayScheduleServiceManager, _altTopicServiceManager, _classHourServiceManager);

                for (int i = 0; i < 8; i++) // 8 class hours
                {
                    classHours[i].AltTopicId = dayScheduleVM.AltTopicIds[i];
                }

                _classHourServiceManager.UpdateRange(classHours);

                var classroom = _classroomServiceManager.GetById((int)daySchedule.ClassroomId);

                var newAltTopics = _classroomServiceManager.GetAllAltTopics((int)daySchedule.ClassroomId, _dayScheduleServiceManager, _altTopicServiceManager, _classHourServiceManager);
                _classroomServiceManager.HandleAltTopics(classroom, currentAltTopics, newAltTopics, _gradeServiceManager, _studentServiceManager);

                return RedirectToAction("Show", "Program", new { id = classroom.Id });

            }
            return View(); // Exception
        }
    }
}
