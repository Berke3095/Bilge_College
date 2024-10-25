using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Services.Concretes;
using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.UI.Areas.Admin.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly I_StudentServiceManager _studentServiceManager;
        private readonly I_GuardianServiceManager _guardianServiceManager;
        private readonly UserManager<User> _userManager;

        public StudentController(I_StudentServiceManager studentServiceManager, I_GuardianServiceManager guardianServiceManager, UserManager<User> userManager)
        {
            _studentServiceManager = studentServiceManager;
            _guardianServiceManager = guardianServiceManager;
            _userManager = userManager;
        }

        public IActionResult FullList()
        {
            ViewBag.ActiveStudents = _studentServiceManager.GetAllActives();
            ViewBag.PassiveStudents = _studentServiceManager.GetAllPassives();
            ViewBag.ActiveGuardians = _guardianServiceManager.GetAllActives();
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.ActiveStudents = _studentServiceManager.GetAllActives();
            ViewBag.ActiveGuardians = _guardianServiceManager.GetAllActives();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                if (studentVM != null)
                {
                    User user = await UserIntegrateServiceManager.CreateUserAsync(_userManager, studentVM.FirstName, studentVM.LastName, studentVM.TCK);
                    if (user != null)
                    {
                        Student student = await _studentServiceManager.SetupStudent(user, _userManager, studentVM.FirstName, studentVM.LastName, studentVM.TCK, studentVM.Gender, studentVM.FinishedSchool, studentVM.FinalGrade, studentVM.GuardianId);
                        _studentServiceManager.Create(student);
                        return RedirectToAction("Create", "Student");
                    }
                }
            }
            ViewBag.ActiveStudents = _studentServiceManager.GetAllActives();
            ViewBag.ActiveGuardians = _guardianServiceManager.GetAllActives();
            return View(studentVM);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _studentServiceManager.Delete(_studentServiceManager.GetById(id));

            // HANDLE ON DELETE - CLASSROOM

            return RedirectToAction("FullList", "Student");
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            _studentServiceManager.DeleteRange(_studentServiceManager.GetAllActives());

            // HANDLE ON DELETE - CLASSROOM

            return RedirectToAction("FullList", "Student");
        }

        [HttpPost]
        public IActionResult Recover(int id)
        {
            _studentServiceManager.Recover(_studentServiceManager.GetById(id));
            return RedirectToAction("FullList", "Student");
        }

        [HttpPost]
        public IActionResult RecoverAll()
        {
            _studentServiceManager.RecoverRange(_studentServiceManager.GetAllPassives());
            return RedirectToAction("FullList", "Student");
        }

        [HttpPost]
        public ActionResult Destroy(int id)
        {
            _studentServiceManager.Destroy(_studentServiceManager.GetById(id));
            return RedirectToAction("FullList", "Student");
        }

        [HttpPost]
        public ActionResult DestroyAll()
        {
            _studentServiceManager.DestroyRange(_studentServiceManager.GetAllPassives());
            return RedirectToAction("FullList", "Student");
        }

        public IActionResult Update(int id)
        {
            var student = _studentServiceManager.GetById(id);
            StudentVM studentVM = new StudentVM
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                TCK = student.TCK,
                FinishedSchool = student.FinishedSchool,
                FinalGrade = student.FinalGrade,
                Gender = student.Gender,
                GuardianId = student.GuardianId,
            };

            ViewBag.ActiveGuardians = _guardianServiceManager.GetAllActives();
            return View(studentVM);
        }

        [HttpPost]
        public IActionResult Update(StudentVM studentVM)
        {
            if (studentVM != null)
            {
                var student = _studentServiceManager.GetById(studentVM.Id);

                student.GuardianId = studentVM.GuardianId;

                _studentServiceManager.Update(student);
                return RedirectToAction("FullList", "Student");
            }

            ViewBag.ActiveGuardians = _guardianServiceManager.GetAllActives();
            return View();
        }
    }
}
