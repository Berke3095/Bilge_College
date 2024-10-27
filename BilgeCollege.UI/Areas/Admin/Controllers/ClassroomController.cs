using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.UI.Areas.Admin.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ClassroomController : Controller
    {
        private readonly I_ClassroomServiceManager _classroomServiceManager;

        public ClassroomController(I_ClassroomServiceManager classroomServiceManager)
        {
            _classroomServiceManager = classroomServiceManager;
        }

        public IActionResult FullList()
        {
            ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
            ViewBag.PassiveClassrooms = _classroomServiceManager.GetAllPassives();
            return View();
        }

        public IActionResult Create(string? selectedGrade)
        {
            var grades = ClassroomManager.GetGrades();

            if(selectedGrade != null)
            {
                grades.Remove(selectedGrade);
                ViewBag.SelectedGrade = selectedGrade;
            }

            ViewBag.Grades = grades;
            ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClassroomVM classroomVM)
        {
            if (ModelState.IsValid)
            {
                if (classroomVM != null)
                {
                    Classroom classroom = new Classroom
                    {
                        Grade = classroomVM.Grade
                    };

                    if (classroom.Grade != null)
                    {
                        classroom.Grade = _classroomServiceManager.GenerateClassCode(_classroomServiceManager, classroom.Grade);
                    }

                    _classroomServiceManager.Create(classroom);
                    return RedirectToAction("Create", "Classroom", new { selectedGrade = classroomVM.Grade });
                }
            }

            ViewBag.Grades = ClassroomManager.GetGrades();
            ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
            return View(classroomVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _classroomServiceManager.Delete(_classroomServiceManager.GetById(id));
            return RedirectToAction("FullList", "Classroom");
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            _classroomServiceManager.DeleteRange(_classroomServiceManager.GetAllActives());
            return RedirectToAction("FullList", "Classroom");
        }

        [HttpPost]
        public IActionResult Destroy(int id)
        {
            _classroomServiceManager.Destroy(_classroomServiceManager.GetById(id));
            return RedirectToAction("FullList", "Classroom");
        }

        [HttpPost]
        public IActionResult DestroyAll()
        {
            _classroomServiceManager.DestroyRange(_classroomServiceManager.GetAllPassives());
            return RedirectToAction("FullList", "Classroom");
        }

        [HttpPost]
        public IActionResult Recover(int id)
        {
            _classroomServiceManager.Recover(_classroomServiceManager.GetById(id));
            return RedirectToAction("FullList", "Classroom");
        }

        [HttpPost]
        public IActionResult RecoverAll()
        {
            _classroomServiceManager.RecoverRange(_classroomServiceManager.GetAllPassives());
            return RedirectToAction("FullList", "Classroom");
        }

        public IActionResult Update(int id)
        {
            var classroom = _classroomServiceManager.GetById(id);
            ClassroomVM classroomVM = new ClassroomVM
            {
                Id = id,
                Grade = classroom.Grade,
            };

            return View(classroomVM);
        }

        [HttpPost]
        public IActionResult Update(ClassroomVM classroomVM)
        {
            if (classroomVM != null)
            {
                var classroom = _classroomServiceManager.GetById(classroomVM.Id);

                if (classroomVM.Grade != null)
                {
                    classroom.Grade = _classroomServiceManager.GenerateClassCode(_classroomServiceManager, classroomVM.Grade);
                }
                else classroom.Grade = null;

                _classroomServiceManager.Update(classroom);
                return RedirectToAction("FullList", "Classroom");
            }

            return View();
        }
    }
}
