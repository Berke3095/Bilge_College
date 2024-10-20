﻿using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MainTopicController : Controller
    {
        private readonly I_MainTopicServiceManager _mainTopicServiceManager;

        public MainTopicController(I_MainTopicServiceManager mainTopicServiceManager)
        {
            _mainTopicServiceManager = mainTopicServiceManager;
        }

        public IActionResult Create()
        {
            return View(new MainTopicVM
            {
                MainTopics = _mainTopicServiceManager.GetAllActives()
            });
        }

        [HttpPost]
        public IActionResult Create(MainTopicVM mainTopicVM)
        {
            if (ModelState.IsValid)
            {
                if (mainTopicVM != null)
                {
                    MainTopic mainTopic = new MainTopic
                    {
                        TopicName = mainTopicVM.TopicName
                    };

                    _mainTopicServiceManager.Create(mainTopic);
                }

                return RedirectToAction("Create", "MainTopic");
            }

            return View(new MainTopicVM
            {
                MainTopics = _mainTopicServiceManager.GetAllActives()
            });
        }
    }
}
