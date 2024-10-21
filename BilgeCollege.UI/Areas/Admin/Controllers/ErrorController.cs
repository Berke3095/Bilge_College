using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ErrorController : Controller
    {
        private readonly I_TeacherServiceManager _teacherServiceManager;

        public ErrorController(I_TeacherServiceManager teacherServiceManager)
        {
            _teacherServiceManager = teacherServiceManager;
        }

        public IActionResult MainTopicDestroyError(int id)
        {
            ViewBag.Teachers = _teacherServiceManager.GetAllActives().Where(x => x.MainTopicId == id).ToList();

            return View();
        }
    }
}
