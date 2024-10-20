using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        private readonly I_TeacherServiceManager _teacherService;
        private readonly I_MainTopicServiceManager _mainTopicServiceManager;

        public TeacherController(I_TeacherServiceManager teacherService, I_MainTopicServiceManager mainTopicServiceManager)
        {
            _teacherService = teacherService;
            _mainTopicServiceManager = mainTopicServiceManager;
        }

        public IActionResult Create()
        {
            TeacherVM teacherVM = new TeacherVM
            {
                MainTopics = _mainTopicServiceManager.GetAllActives(),
                Teachers = _teacherService.GetAllActives()
            };
            return View(teacherVM);
        }

        [HttpPost]
        public IActionResult Create(TeacherVM teacherVM)
        {
            return View();
        }
    }
}
