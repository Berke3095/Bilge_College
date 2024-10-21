using BilgeCollege.BLL.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ErrorController : Controller
    {
        private readonly I_TeacherServiceManager _teacherServiceManager;
        private readonly I_MainTopicServiceManager _mainTopicServiceManager;

        public ErrorController(I_TeacherServiceManager teacherServiceManager, I_MainTopicServiceManager mainTopicServiceManager)
        {
            _teacherServiceManager = teacherServiceManager;
            _mainTopicServiceManager = mainTopicServiceManager;
        }

        public IActionResult MainTopicDestroyError(int id)
        {
            ViewBag.Teachers = _teacherServiceManager.GetAllActives().Where(x => x.MainTopicId == id).ToList();
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            return View();
        }
    }
}
