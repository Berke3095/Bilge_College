using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.UI.Areas.Admin.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;

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
        public async Task<IActionResult> Create(TeacherVM createTeacherVM)
        {
            if(ModelState.IsValid)
            {
                if (createTeacherVM != null)
                {
                    User user = await _teacherServiceManager.CreateUserAsync(_userManager, createTeacherVM.FirstName, createTeacherVM.LastName, createTeacherVM.TCK);
                    if (user != null)
                    {
                        Teacher teacher = await _teacherServiceManager.SetupTeacher(user, _userManager, createTeacherVM.FirstName, createTeacherVM.LastName, createTeacherVM.TCK, createTeacherVM.MainTopicId);
                        _teacherServiceManager.Create(teacher);
                        return RedirectToAction("Create", "Teacher");
                    }
                }
            }
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.Teachers = _teacherServiceManager.GetAllActives();
            return View(createTeacherVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _teacherServiceManager.Delete(_teacherServiceManager.GetById(id));
            return RedirectToAction("FullList", "Teacher");
        }

        [HttpPost]
        public async Task<IActionResult> Destroy(int id)
        {
            var foundTeacher = _teacherServiceManager.GetById(id);
            var teacherUserToDelete = await _userManager.FindByIdAsync(foundTeacher.UserId);
            if (teacherUserToDelete != null) await _userManager.DeleteAsync(teacherUserToDelete);

            _teacherServiceManager.Destroy(foundTeacher);

            return RedirectToAction("FullList", "Teacher");
        }

        [HttpPost]
        public IActionResult Recover(int id)
        {
            _teacherServiceManager.Recover(_teacherServiceManager.GetById(id));
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
            return View();
        }
    }
}
