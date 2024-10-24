using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.UI.Areas.Admin.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Services.Concretes;

namespace BilgeCollege.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GuardianController : Controller
    {
        private readonly I_GuardianServiceManager _guardianServiceManager;
        private readonly UserManager<User> _userManager;

        public GuardianController(I_GuardianServiceManager guardianServiceManager, UserManager<User> userManager)
        {
            _guardianServiceManager = guardianServiceManager;
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
                    User user = await _guardianServiceManager.CreateUserAsync(_userManager, guardianVM.FirstName, guardianVM.LastName, guardianVM.TCK);
                    if (user != null)
                    {
                        Guardian guardian = await _guardianServiceManager.SetupGuardian(user, _userManager, guardianVM.FirstName, guardianVM.LastName, guardianVM.TCK, guardianVM.HomeAddress);
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
            _guardianServiceManager.Delete(_guardianServiceManager.GetById(id));

            // HANDLE ON DELETE - STUDENTS

            return RedirectToAction("FullList", "Guardian");
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            _guardianServiceManager.DeleteRange(_guardianServiceManager.GetAllActives());

            // HANDLE ON DELETE - STUDENTS

            return RedirectToAction("FullList", "Guardian");
        }

        [HttpPost]
        public IActionResult Recover(int id)
        {
            _guardianServiceManager.Recover(_guardianServiceManager.GetById(id));

            // HANDLE ON RECOVER - STUDENTS

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
            await _guardianServiceManager.HandleOnDestroy(_userManager, foundGuardian);

            _guardianServiceManager.Destroy(foundGuardian);
            return RedirectToAction("FullList", "Guardian");
        }

        [HttpPost]
        public IActionResult DestroyAll()
        {
            var passiveGuardians = _guardianServiceManager.GetAllPassives();
            foreach(var item in passiveGuardians)
            {
                _guardianServiceManager.HandleOnDestroy(_userManager, item);
            }

            _guardianServiceManager.DestroyRange(passiveGuardians);
            return RedirectToAction("FullList", "Guardian");
        }
    }
}
