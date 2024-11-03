using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.UI.Areas.Admin.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Utils;
using BilgeCollege.BLL.Services.Concretes;

namespace BilgeCollege.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GuardianController : Controller
    {
        private readonly I_GuardianServiceManager _guardianServiceManager;
        private readonly I_StudentServiceManager _studentServiceManager;
        private readonly I_MessageServiceManager _messageServiceManager;
        private readonly UserManager<User> _userManager;

        public GuardianController(I_GuardianServiceManager guardianServiceManager, I_StudentServiceManager studentServiceManager, I_MessageServiceManager messageServiceManager, UserManager<User> userManager)
        {
            _guardianServiceManager = guardianServiceManager;
            _studentServiceManager = studentServiceManager;
            _messageServiceManager = messageServiceManager;
            _userManager = userManager;
        }

        public IActionResult FullList()
        {
            ViewBag.PassiveGuardians = _guardianServiceManager.GetAllPassives();
            ViewBag.ActiveGuardians = _guardianServiceManager.GetAllActives();
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Guardians = _guardianServiceManager.GetAllActives();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GuardianVM guardianVM)
        {
            if (ModelState.IsValid)
            {
                if (guardianVM != null)
                {
                    User user = await UserIntegrateServiceManager.CreateUserAsync(_userManager, guardianVM.FirstName, guardianVM.LastName, guardianVM.TCK);
                    if (user != null)
                    {
                        var guardian = await _guardianServiceManager.SetupGuardian(user, _userManager, guardianVM.FirstName, guardianVM.LastName, guardianVM.TCK, guardianVM.PhoneNumber, guardianVM.HomeAddress);
                        _guardianServiceManager.Create(guardian);
                        return RedirectToAction("Create", "Guardian");
                    }
                }
            }

            ViewBag.Guardians = _guardianServiceManager.GetAllActives();
            return View(guardianVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var students = _studentServiceManager.GetAll().Where(x => x.GuardianId == id).ToList();
            foreach (var item in students)
            {
                item.GuardianId = null;
            }

            _guardianServiceManager.Delete(_guardianServiceManager.GetById(id));

            return RedirectToAction("FullList", "Guardian");
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            var students = _studentServiceManager.GetAll();
            foreach (var item in students)
            {
                item.GuardianId = null;
            }

            _guardianServiceManager.DeleteRange(_guardianServiceManager.GetAllActives());

            return RedirectToAction("FullList", "Guardian");
        }

        [HttpPost]
        public IActionResult Recover(int id)
        {
            _guardianServiceManager.Recover(_guardianServiceManager.GetById(id));
            return RedirectToAction("FullList", "Guardian");
        }

        [HttpPost]
        public IActionResult RecoverAll()
        {
            _guardianServiceManager.RecoverRange(_guardianServiceManager.GetAllPassives());
            return RedirectToAction("FullList", "Guardian");
        }

        [HttpPost]
        public async Task<IActionResult> Destroy(int id)
        {
            var foundGuardian = _guardianServiceManager.GetById(id);

            var messages = _messageServiceManager.GetAll().Where(x => x.SenderId == foundGuardian.UserId || x.ReceiverId == foundGuardian.UserId).ToList();
            _messageServiceManager.DestroyRange(messages);

            await _guardianServiceManager.HandleOnDestroy(_userManager, foundGuardian);

            _guardianServiceManager.Destroy(foundGuardian);
            return RedirectToAction("FullList", "Guardian");
        }

        [HttpPost]
        public async Task<IActionResult> DestroyAll()
        {
            var passiveGuardians = _guardianServiceManager.GetAllPassives();
            foreach(var item in passiveGuardians)
            {
                var messages = _messageServiceManager.GetAll().Where(x => x.SenderId == item.UserId || x.ReceiverId == item.UserId).ToList();
                _messageServiceManager.DestroyRange(messages);
                await _guardianServiceManager.HandleOnDestroy(_userManager, item);
            }

            _guardianServiceManager.DestroyRange(passiveGuardians);
            return RedirectToAction("FullList", "Guardian");
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var guardian = _guardianServiceManager.GetById(id);

            _studentServiceManager.GetAllActives().Where(x => x.GuardianId == id).ToList();

            return View(guardian);
        }
    }
}
