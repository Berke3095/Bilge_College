using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BilgeCollege.UI.Areas.Guardian.Controllers
{
    [Area("Guardian")]
    [Authorize(Roles = "Guardian")]
    public class ChildrenController : Controller
    {
        private readonly I_GuardianServiceManager _guardianServiceManager;
        private readonly I_ClassroomServiceManager _classroomServiceManager;
        private readonly UserManager<User> _userManager;

        public ChildrenController(I_GuardianServiceManager guardianServiceManager, I_ClassroomServiceManager classroomServiceManager, UserManager<User> userManager)
        {
            _guardianServiceManager = guardianServiceManager;
            _classroomServiceManager = classroomServiceManager;
            _userManager = userManager;
        }

        public IActionResult Show()
        {
            var userId = _userManager.GetUserId(User);
            var thisGuardian = _guardianServiceManager.GetDbSet().Include(x => x.Students).First(x => x.UserId == userId);


            _classroomServiceManager.GetAllActives();
            return View(thisGuardian);
        }
    }
}
