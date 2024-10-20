using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.UI.Areas.Admin.Models;
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
            return View(new TeacherVM
            {
                MainTopics = _mainTopicServiceManager.GetAllActives(),
                Teachers = _teacherService.GetAllActives()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherVM teacherVM)
        {
            if(ModelState.IsValid)
            {
                if (teacherVM != null)
                {
                    User user = await _teacherService.CreateUserAsync(teacherVM.FirstName, teacherVM.LastName, teacherVM.TCK);
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
                                    FirstName = teacherVM.FirstName,
                                    LastName = teacherVM.LastName,
                                    TCK = teacherVM.TCK,
                                    Email = user.Email,
                                    MainTopicId = teacherVM.MainTopicId
                                };

                                _teacherService.Create(teacher);
                                return RedirectToAction("Create", "Teacher");
                            }
                        }
                    }
                }
            }
            return View(new TeacherVM {
                MainTopics = _mainTopicServiceManager.GetAllActives(),
                Teachers = _teacherService.GetAllActives()
            });
        }
    }
}
