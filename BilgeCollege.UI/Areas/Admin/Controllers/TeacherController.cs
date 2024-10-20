﻿using BilgeCollege.BLL.Services.Abstracts;
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
    public class TeacherController : Controller
    {
        private readonly I_TeacherServiceManager _teacherServiceManager;
        private readonly I_MainTopicServiceManager _mainTopicServiceManager;
        private readonly UserManager<User> _userManager;

        public TeacherController(I_TeacherServiceManager teacherService, I_MainTopicServiceManager mainTopicServiceManager, UserManager<User> userManager)
        {
            _teacherServiceManager = teacherService;
            _mainTopicServiceManager = mainTopicServiceManager;
            _userManager = userManager;
        }

        public IActionResult Recycle()
        {
            ViewBag.ActiveTeachers = _teacherServiceManager.GetAllActives();
            ViewBag.PassiveTeachers = _teacherServiceManager.GetAllPassives();
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.Teachers = _teacherServiceManager.GetAllActives();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherVM createTeacherVM)
        {
            if(ModelState.IsValid)
            {
                if (createTeacherVM != null)
                {
                    User user = await _teacherServiceManager.CreateUserAsync(createTeacherVM.FirstName, createTeacherVM.LastName, createTeacherVM.TCK);
                    if (user != null)
                    {
                        var result = await _userManager.CreateAsync(user);
                        if(result.Succeeded)
                        {
                            string role = "Teacher";
                            var roleResult = await _userManager.AddToRoleAsync(user, role);
                            if (roleResult.Succeeded)
                            {
                                Teacher teacher = new Teacher
                                {
                                    FirstName = createTeacherVM.FirstName,
                                    LastName = createTeacherVM.LastName,
                                    TCK = createTeacherVM.TCK,
                                    Email = user.Email,
                                    UserId = user.Id,
                                    MainTopicId = createTeacherVM.MainTopicId
                                };

                                _teacherServiceManager.Create(teacher);
                                return RedirectToAction("Create", "Teacher");
                            }
                        }
                    }
                }
            }
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.Teachers = _teacherServiceManager.GetAllActives();
            return View(createTeacherVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _teacherServiceManager.Delete(_teacherServiceManager.GetById(id));
            return RedirectToAction("Recycle", "Teacher");
        }

        [HttpPost]
        public async Task<IActionResult> Destroy(int id)
        {
            var foundTeacher = _teacherServiceManager.GetById(id);
            var teacherUserToDelete = await _userManager.FindByIdAsync(foundTeacher.UserId);
            if (teacherUserToDelete != null) await _userManager.DeleteAsync(teacherUserToDelete);

            _teacherServiceManager.Destroy(foundTeacher);

            return RedirectToAction("Recycle", "Teacher");
        }
    }
}
