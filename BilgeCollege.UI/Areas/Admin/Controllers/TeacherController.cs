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
            return View(new CreateTeacherVM
            {
                MainTopics = _mainTopicServiceManager.GetAllActives(),
                Teachers = _teacherService.GetAllActives()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeacherVM createTeacherVM)
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
            createTeacherVM.MainTopics = _mainTopicServiceManager.GetAllActives();
            createTeacherVM.Teachers = _teacherService.GetAllActives();
            return View(createTeacherVM);
        }
    }
}
