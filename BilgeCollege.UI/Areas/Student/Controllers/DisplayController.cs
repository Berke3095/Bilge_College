using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class DisplayController : Controller
    {
        private readonly I_StudentServiceManager _studentServiceManager;
        private readonly UserManager<User> _userManager;

        public DisplayController(I_StudentServiceManager studentServiceManager, UserManager<User> userManager)
        {
            _studentServiceManager = studentServiceManager;
            _userManager = userManager;
        }

        public IActionResult Show()
        {
            var userId = _userManager.GetUserId(User);
            var thisStudent = _studentServiceManager.GetAllActives().First(x => x.UserId == userId);


            return RedirectToAction("Details", "Student", new { area = "Admin", id = thisStudent.Id });
        }
    }
}
