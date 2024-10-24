using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.UI.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly I_TeacherServiceManager _teacherServiceManager;
        private readonly I_GuardianServiceManager _guardianServiceManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, I_TeacherServiceManager teacherServiceManager, I_GuardianServiceManager guardianServiceManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _teacherServiceManager = teacherServiceManager;
            _guardianServiceManager = guardianServiceManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.bRememberMe, false);

                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains("Teacher"))
                        {
                            if(!_teacherServiceManager.GetAllActives().Any(x => x.UserId == user.Id))
                            {
                                ViewData["LoginError"] = "You account has been deactivated, contact the college.";
                                return View(loginVM);
                            }
                        }
                        else if(roles.Contains("Guardian"))
                        {
                            if (!_guardianServiceManager.GetAllActives().Any(x => x.UserId == user.Id))
                            {
                                ViewData["LoginError"] = "You account has been deactivated, contact the college.";
                                return View(loginVM);
                            }
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else 
                    {
                        ViewData["LoginError"] = "Login failed, check your email and password.";
                        return View(loginVM);
                    } 
                }
                else return View();
            }
            else return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
