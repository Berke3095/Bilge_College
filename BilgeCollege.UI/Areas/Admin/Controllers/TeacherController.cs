using BilgeCollege.BLL.Services.Abstracts;
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
        private readonly I_TeacherServiceManager _teacherService;
        private readonly I_MainTopicServiceManager _mainTopicServiceManager;
        private readonly UserManager<User> _userManager;

        public TeacherController(I_TeacherServiceManager teacherService, I_MainTopicServiceManager mainTopicServiceManager, UserManager<User> userManager)
        {
            _teacherService = teacherService;
            _mainTopicServiceManager = mainTopicServiceManager;
            _userManager = userManager;
        }

        public IActionResult Create()
        {
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.Teachers = _teacherService.GetAllActives();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherVM createTeacherVM)
        {
            if(ModelState.IsValid)
            {
                if (createTeacherVM != null)
                {
                    User user = await _teacherService.CreateUserAsync(createTeacherVM.FirstName, createTeacherVM.LastName, createTeacherVM.TCK);
                    if (user != null)
                    {
                        var result = await _userManager.CreateAsync(user);
                        if(result.Succeeded)
                        {
                            string role = "Teacher";
                            var roleResult = await _userManager.AddToRoleAsync(user, role);
                            if (roleResult.Succeeded)
                            {
                                Teacher teacher = new Teacher
                                {
                                    FirstName = createTeacherVM.FirstName,
                                    LastName = createTeacherVM.LastName,
                                    TCK = createTeacherVM.TCK,
                                    Email = user.Email,
                                    MainTopicId = createTeacherVM.MainTopicId
                                };

                                _teacherService.Create(teacher);
                                return RedirectToAction("Create", "Teacher");
                            }
                        }
                    }
                }
            }
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.Teachers = _teacherService.GetAllActives();
            return View(createTeacherVM);
        }
    }
}
