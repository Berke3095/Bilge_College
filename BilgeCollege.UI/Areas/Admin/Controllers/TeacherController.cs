using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Services.Concretes;
using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.UI.Areas.Admin.Views.Models;
using BilgeCollege.UI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        private readonly I_TeacherServiceManager _teacherServiceManager;
        private readonly I_MainTopicServiceManager _mainTopicServiceManager;
        private readonly I_AltTopicServiceManager _altTopicServiceManager;
        private readonly I_DayScheduleServiceManager _dayScheduleServiceManager;
        private readonly I_ClassHourServiceManager _classHourServiceManager;
        private readonly UserManager<User> _userManager;

        private static int? _previousMainTopicId; // For update

        public TeacherController(I_TeacherServiceManager teacherService, I_MainTopicServiceManager mainTopicServiceManager, I_AltTopicServiceManager altTopicServiceManager, I_DayScheduleServiceManager dayScheduleServiceManager, I_ClassHourServiceManager classHourServiceManager, UserManager<User> userManager)
        {
            _teacherServiceManager = teacherService;
            _mainTopicServiceManager = mainTopicServiceManager;
            _altTopicServiceManager = altTopicServiceManager;
            _dayScheduleServiceManager = dayScheduleServiceManager;
            _classHourServiceManager = classHourServiceManager;
            _userManager = userManager;
        }

        public IActionResult FullList()
        {
            ViewBag.ActiveTeachers = _teacherServiceManager.GetAllActives();
            ViewBag.PassiveTeachers = _teacherServiceManager.GetAllPassives();
            _mainTopicServiceManager.GetAllActives();
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.Teachers = _teacherServiceManager.GetAllActives();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherVM teacherVM)
        {
            if(ModelState.IsValid)
            {
                if (teacherVM != null)
                {
                    User user = await UserIntegrateServiceManager.CreateUserAsync(_userManager, teacherVM.FirstName, teacherVM.LastName, teacherVM.TCK);
                    if (user != null)
                    {
                        var teacher = await _teacherServiceManager.SetupTeacher(user, _userManager, teacherVM.FirstName, teacherVM.LastName, teacherVM.TCK, teacherVM.PhoneNumber, teacherVM.MainTopicId);
                        _teacherServiceManager.Create(teacher);
                        return RedirectToAction("Create", "Teacher");
                    }
                }
            }
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.Teachers = _teacherServiceManager.GetAllActives();
            return View(teacherVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var altTopics = _altTopicServiceManager.GetAllActives().Where(x => x.TeacherId == id);
            foreach(var item in altTopics)
            {
                item.TeacherId = null;
            }

            _teacherServiceManager.Delete(_teacherServiceManager.GetById(id));
            return RedirectToAction("FullList", "Teacher");
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            var altTopics = _altTopicServiceManager.GetAllActives();
            foreach (var item in altTopics)
            {
                item.TeacherId = null;
            }

            _teacherServiceManager.DeleteRange(_teacherServiceManager.GetAllActives());
            return RedirectToAction("FullList", "Teacher");
        }

        [HttpPost]
        public async Task<IActionResult> Destroy(int id)
        {
            var foundTeacher = _teacherServiceManager.GetById(id);
            var daySchedulesToDestroy = _dayScheduleServiceManager.GetAll().Where(x => x.TeacherId == foundTeacher.Id).ToList();
            foreach(var daySchedule in daySchedulesToDestroy)
            {
                var classHours = _classHourServiceManager.GetAll().Where(x => x.DayScheduleId == daySchedule.Id).ToList();
                _classHourServiceManager.DestroyRange(classHours);
            }
            _dayScheduleServiceManager.DestroyRange(daySchedulesToDestroy);
            await _teacherServiceManager.HandleOnDestroy(_userManager, foundTeacher);

            _teacherServiceManager.Destroy(foundTeacher);

            return RedirectToAction("FullList", "Teacher");
        }

        [HttpPost]
        public async Task<IActionResult> DestroyAll()
        {
            var passiveTeachers = _teacherServiceManager.GetAllPassives();
            foreach(var item in passiveTeachers)
            {
                var daySchedulesToDestroy = _dayScheduleServiceManager.GetAll().Where(x => x.TeacherId == item.Id).ToList();
                foreach (var daySchedule in daySchedulesToDestroy)
                {
                    var classHours = _classHourServiceManager.GetAll().Where(x => x.DayScheduleId == daySchedule.Id).ToList();
                    _classHourServiceManager.DestroyRange(classHours);
                }
                _dayScheduleServiceManager.DestroyRange(daySchedulesToDestroy);
                await _teacherServiceManager.HandleOnDestroy(_userManager, item);
            }

            _teacherServiceManager.DestroyRange(passiveTeachers);

            return RedirectToAction("FullList", "Teacher");
        }

        [HttpPost]
        public IActionResult Recover(int id)
        {
            _teacherServiceManager.Recover(_teacherServiceManager.GetById(id));
            return RedirectToAction("FullList", "Teacher");
        }

        [HttpPost]
        public IActionResult RecoverAll()
        {
            _teacherServiceManager.RecoverRange(_teacherServiceManager.GetAllPassives());
            return RedirectToAction("FullList", "Teacher");
        }

        public IActionResult Update(int id)
        {
            _previousMainTopicId = null;

            var teacher = _teacherServiceManager.GetById(id);
            TeacherVM teacherVM = Mapper.TeacherToTeacherVM(teacher);

            _previousMainTopicId = teacher.MainTopicId;

            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            return View(teacherVM);
        }

        [HttpPost]
        public IActionResult Update(TeacherVM teacherVM)
        {
            if (teacherVM != null)
            {
                var teacher = _teacherServiceManager.GetById(teacherVM.Id);

                if(teacherVM.MainTopicId != _previousMainTopicId)
                {
                    var altTopics = _altTopicServiceManager.GetAllActives().Where(x => x.TeacherId == teacher.Id);
                    foreach(var item in altTopics)
                    {
                        item.TeacherId = null;
                    }
                }

                teacher.MainTopicId = teacherVM.MainTopicId;

                _teacherServiceManager.Update(teacher);
                return RedirectToAction("FullList", "Teacher");

            }

            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            return View();
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var teacher = _teacherServiceManager.GetById(id);

            _mainTopicServiceManager.GetAllActives();
            _altTopicServiceManager.GetAllActives();

            return View(teacher);
        }
    }
}
