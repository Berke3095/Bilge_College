using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.UI.Areas.Admin.Views.Models;
using BilgeCollege.UI.Utils;
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
        private readonly I_ClassroomServiceManager _classroomServiceManager;
        private readonly I_GradeServiceManager _gradeServiceManager;
        private readonly I_AltTopicServiceManager _altTopicServiceManager;


        private readonly UserManager<User> _userManager;

        public static int? _previousClassroomId; // For update

        public StudentController(I_StudentServiceManager studentServiceManager, I_GuardianServiceManager guardianServiceManager, I_ClassroomServiceManager classroomServiceManager, UserManager<User> userManager, I_GradeServiceManager gradeServiceManager, I_AltTopicServiceManager altTopicServiceManager)
        {
            _studentServiceManager = studentServiceManager;
            _guardianServiceManager = guardianServiceManager;
            _classroomServiceManager = classroomServiceManager;
            _gradeServiceManager = gradeServiceManager;
            _altTopicServiceManager = altTopicServiceManager;


            _userManager = userManager;
        }

        public IActionResult FullList()
        {
            _classroomServiceManager.GetAllActives();
            ViewBag.ActiveStudents = _studentServiceManager.GetAllActives();
            ViewBag.PassiveStudents = _studentServiceManager.GetAllPassives();
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
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
                        Student student = await _studentServiceManager.SetupStudent(user, _userManager, studentVM.FirstName, studentVM.LastName, studentVM.TCK, studentVM.Gender, studentVM.FinishedSchool, studentVM.FinalGrade, studentVM.ClassroomId, studentVM.GuardianId);

                        if(studentVM.ClassroomId != null)
                        {
                            var classroom = _classroomServiceManager.GetById((int)studentVM.ClassroomId);
                            if (classroom.TotalStudents >= classroom.MaxCapacity)
                            {
                                ViewData["FullClassroomError"] = "Classroom is full.";

                                ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
                                ViewBag.ActiveStudents = _studentServiceManager.GetAllActives();
                                ViewBag.ActiveGuardians = _guardianServiceManager.GetAllActives();
                                return View(studentVM);
                            }
                            classroom.TotalStudents++;
                        }

                        _studentServiceManager.Create(student);
                        return RedirectToAction("Create", "Student");
                    }
                }
            }
            ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
            ViewBag.ActiveStudents = _studentServiceManager.GetAllActives();
            ViewBag.ActiveGuardians = _guardianServiceManager.GetAllActives();
            return View(studentVM);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var student = _studentServiceManager.GetById(id);

            var classroom = _classroomServiceManager.GetById((int)student.ClassroomId);
            classroom.TotalStudents--;

            student.ClassroomId = null;
            student.GuardianId = null;

            var grades = _gradeServiceManager.GetAll().Where(x => x.StudentId == id).ToList();
            _gradeServiceManager.DestroyRangeWithoutSave(grades);

            _studentServiceManager.Delete(student);
            return RedirectToAction("FullList", "Student");
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            var classrooms = _classroomServiceManager.GetAllActives();
            var students = _studentServiceManager.GetAllActives();
            foreach (var item in classrooms)
            {
                item.TotalStudents = 0;
            }

            foreach(var item in students)
            {
                item.ClassroomId = null;
                item.GuardianId = null;
            }

            _studentServiceManager.DeleteRange(students);
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
            StudentVM studentVM = Mapper.StudentToStudentVM(student);

            _previousClassroomId = null;

            if (studentVM.ClassroomId != null)
            {
                _previousClassroomId = (int)studentVM.ClassroomId;
                ViewData["OriginalGrade"] = _classroomServiceManager.GetById((int)_previousClassroomId).Grade;
            }
            else
            {
                ViewData["OriginalGrade"] = "--Select a classroom--";
            }
            

            ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
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

                if(studentVM.ClassroomId != null)
                {
                    var classroomForCheck = _classroomServiceManager.GetById((int)studentVM.ClassroomId);
                    if (classroomForCheck != null)
                    {
                        if (classroomForCheck.TotalStudents >= classroomForCheck.MaxCapacity)
                        {
                            ViewData["FullClassroomError"] = "Classroom is full.";

                            if(_previousClassroomId != null)
                            {
                                ViewData["OriginalGrade"] = _classroomServiceManager.GetById((int)_previousClassroomId).Grade;
                            }
                            else
                            {
                                ViewData["OriginalGrade"] = "--Select a classroom--";
                            }

                            ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
                            ViewBag.ActiveGuardians = _guardianServiceManager.GetAllActives();

                            StudentVM studentVMforError = Mapper.StudentToStudentVM(student);
                            return View(studentVMforError);
                        }
                    }
                }

                student.ClassroomId = studentVM.ClassroomId;

                if(_previousClassroomId != null)
                {
                    if(_previousClassroomId != student.ClassroomId)
                    {
                        var classroomBefore = _classroomServiceManager.GetById((int)_previousClassroomId);
                        classroomBefore.TotalStudents--;

                        if (studentVM.ClassroomId != null)
                        {
                            var classroomNow = _classroomServiceManager.GetById((int)student.ClassroomId);
                            classroomNow.TotalStudents++;
                        }   
                    }
                }
                else
                {
                    if(student.ClassroomId != null)
                    {
                        var classroom = _classroomServiceManager.GetById((int)student.ClassroomId);
                        classroom.TotalStudents++;
                    }
                }

                _studentServiceManager.Update(student);
                return RedirectToAction("FullList", "Student");
            }

            return View(); // Exception
        }

        public IActionResult Details(int id)
        {
            var student = _studentServiceManager.GetById(id);

            _guardianServiceManager.GetAllActives();
            _classroomServiceManager.GetAllActives();
            _gradeServiceManager.GetAll().Where(x => x.StudentId == id).ToList();
            _altTopicServiceManager.GetAllActives();
            

            return View(student);
        }
    }
}
