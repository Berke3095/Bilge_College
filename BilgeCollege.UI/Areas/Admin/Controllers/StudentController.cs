using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Services.Concretes;
using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.UI.Areas.Admin.Views.Models;
using BilgeCollege.UI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly I_MessageServiceManager _messageServiceManager;
        private readonly UserManager<User> _userManager;

        public static int? _previousClassroomId; // For update

        public StudentController(I_StudentServiceManager studentServiceManager, I_GuardianServiceManager guardianServiceManager, I_ClassroomServiceManager classroomServiceManager, UserManager<User> userManager, I_GradeServiceManager gradeServiceManager, I_AltTopicServiceManager altTopicServiceManager, I_MessageServiceManager messageServiceManager)
        {
            _studentServiceManager = studentServiceManager;
            _guardianServiceManager = guardianServiceManager;
            _classroomServiceManager = classroomServiceManager;
            _gradeServiceManager = gradeServiceManager;
            _altTopicServiceManager = altTopicServiceManager;
            _messageServiceManager = messageServiceManager;
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
                        var student = await _studentServiceManager.SetupStudent(user, _userManager, studentVM.FirstName, studentVM.LastName, studentVM.TCK, studentVM.Gender, studentVM.FinishedSchool, studentVM.FinalGrade, studentVM.ClassroomId, studentVM.GuardianId);

                        var gradesToCreate = new List<Grade>();

                        if (studentVM.ClassroomId != null)
                        {
                            var classroom = _classroomServiceManager.GetClassroomWithAltTopics((int)studentVM.ClassroomId);
                            if (classroom.TotalStudents >= classroom.MaxCapacity)
                            {
                                ViewData["FullClassroomError"] = "Classroom is full.";

                                ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
                                ViewBag.ActiveStudents = _studentServiceManager.GetAllActives();
                                ViewBag.ActiveGuardians = _guardianServiceManager.GetAllActives();
                                return View(studentVM);
                            }
                            classroom.TotalStudents++;

                            if (classroom.AltTopics != null)
                            {
                                foreach (var altTopic in classroom.AltTopics)
                                {
                                    Grade grade = new Grade
                                    {
                                        AltTopicId = altTopic.Id
                                    };
                                    gradesToCreate.Add(grade);
                                }
                            }
                        }

                        _studentServiceManager.Create(student);

                        if(gradesToCreate.Count() > 0)
                        {
                            foreach(var grade in gradesToCreate)
                            {
                                grade.StudentId = student.Id;
                            }
                            _gradeServiceManager.CreateRange(gradesToCreate);
                        }

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
            _gradeServiceManager.DestroyRange(grades);

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
            var student = _studentServiceManager.GetDbSet().Include(x => x.Grades).First(x => x.Id == id);

            var messages = _messageServiceManager.GetAll().Where(x => x.SenderId == student.UserId || x.ReceiverId == student.UserId).ToList();
            _messageServiceManager.DestroyRange(messages);

            _gradeServiceManager.DestroyRange(student.Grades);

            _studentServiceManager.Destroy(student);
            
            return RedirectToAction("FullList", "Student");
        }

        [HttpPost]
        public ActionResult DestroyAll()
        {
            var students = _studentServiceManager.GetAllPassives();

            _gradeServiceManager.DestroyRange(_gradeServiceManager.GetAll());

            foreach(var student in students)
            {
                var messages = _messageServiceManager.GetAll().Where(x => x.SenderId == student.UserId || x.ReceiverId == student.UserId).ToList();
                _messageServiceManager.DestroyRange(messages);
            }

            _studentServiceManager.DestroyRange(students);
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
                var gradesToCreate = new List<Grade>();

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
                        var gradesToRemove = _gradeServiceManager.GetAll().Where(x => x.StudentId == student.Id).ToList();
                        _gradeServiceManager.DestroyRange(gradesToRemove);

                        if (studentVM.ClassroomId != null)
                        {
                            _altTopicServiceManager.GetAllActives();

                            var classroomNow = _classroomServiceManager.GetClassroomWithAltTopics((int)student.ClassroomId);
                            classroomNow.TotalStudents++;

                            if(classroomNow.AltTopics != null)
                            {
                                foreach (var altTopic in classroomNow.AltTopics)
                                {
                                    Grade grade = new Grade
                                    {
                                        AltTopicId = altTopic.Id,
                                        StudentId = student.Id,
                                    };
                                    gradesToCreate.Add(grade);
                                }
                            }
                        }   
                    }
                }
                else
                {
                    if(student.ClassroomId != null)
                    {
                        var classroom = _classroomServiceManager.GetClassroomWithAltTopics((int)student.ClassroomId);
                        classroom.TotalStudents++;

                        if (classroom.AltTopics != null)
                        {
                            foreach (var altTopic in classroom.AltTopics)
                            {
                                Grade grade = new Grade
                                {
                                    AltTopicId = altTopic.Id,
                                    StudentId = student.Id,
                                };
                                gradesToCreate.Add(grade);
                            }
                        }
                    }
                }
                _gradeServiceManager.CreateRange(gradesToCreate);
                _studentServiceManager.Update(student);
                return RedirectToAction("FullList", "Student");
            }

            return View(); // Exception
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var student = _studentServiceManager.GetById(id);

            _guardianServiceManager.GetAllActives();
            _classroomServiceManager.GetAllActives();
            _gradeServiceManager.GetAll().Where(x => x.StudentId == id).ToList();
            _altTopicServiceManager.GetAllActives();

            return View(student);
        }

        public IActionResult PrintResult(int id)
        {
            var student = _studentServiceManager.GetById(id);
            var grades = _gradeServiceManager.GetAll().Where(x => x.StudentId == id).ToList();

            double yearScore = 0;
            ViewBag.YearScore = yearScore;

            if(grades.Count() > 0)
            {
                foreach(var grade in grades)
                {
                    yearScore += grade.Score;
                }

                ViewBag.YearScore = yearScore / grades.Count();
            }

            _altTopicServiceManager.GetAllActives();
            ViewBag.Date = DateTime.Today;
            return View(student);
        }
    }
}
