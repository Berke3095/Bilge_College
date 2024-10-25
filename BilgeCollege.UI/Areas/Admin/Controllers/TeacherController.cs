using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.UI.Areas.Admin.Views.Models;
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
        private readonly UserManager<User> _userManager;

        public TeacherController(I_TeacherServiceManager teacherService, I_MainTopicServiceManager mainTopicServiceManager, UserManager<User> userManager)
        {
            _teacherServiceManager = teacherService;
            _mainTopicServiceManager = mainTopicServiceManager;
            _userManager = userManager;
        }

        public IActionResult FullList()
        {
            ViewBag.ActiveTeachers = _teacherServiceManager.GetAllActives();
            ViewBag.PassiveTeachers = _teacherServiceManager.GetAllPassives();
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
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
                        Teacher teacher = await _teacherServiceManager.SetupTeacher(user, _userManager, teacherVM.FirstName, teacherVM.LastName, teacherVM.TCK, teacherVM.PhoneNumber, teacherVM.MainTopicId);
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
            _teacherServiceManager.Delete(_teacherServiceManager.GetById(id));
            return RedirectToAction("FullList", "Teacher");
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            _teacherServiceManager.DeleteRange(_teacherServiceManager.GetAllActives());
            return RedirectToAction("FullList", "Teacher");
        }

        [HttpPost]
        public async Task<IActionResult> Destroy(int id)
        {
            var foundTeacher = _teacherServiceManager.GetById(id);
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
            var teacher = _teacherServiceManager.GetById(id);
            TeacherVM teacherVM = new TeacherVM
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MainTopicId = teacher.MainTopicId,
                TCK = teacher.TCK
            };

            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            return View(teacherVM);
        }

        [HttpPost]
        public IActionResult Update(TeacherVM teacherVM)
        {
            if (teacherVM != null)
            {
                var teacher = _teacherServiceManager.GetById(teacherVM.Id);

                teacher.MainTopicId = teacherVM.MainTopicId;

                _teacherServiceManager.Update(teacher);
                return RedirectToAction("FullList", "Teacher");
            }

            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            return View();
        }
    }
}
