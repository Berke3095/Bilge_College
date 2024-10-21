using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Services.Concretes;
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
        private readonly I_TeacherServiceManager _teacherServiceManager;

        public MainTopicController(I_MainTopicServiceManager mainTopicServiceManager, I_TeacherServiceManager teacherServiceManager)
        {
            _mainTopicServiceManager = mainTopicServiceManager;
            _teacherServiceManager = teacherServiceManager;
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

            mainTopicVM.MainTopics = _mainTopicServiceManager.GetAllActives();
            return View(mainTopicVM);
        }

        [HttpPost]
        public IActionResult Destroy(int id)
        {
            int topicCheck = _teacherServiceManager.GetAllActives().Where(x => x.MainTopicId == id).Count();
            if (topicCheck == 0) _mainTopicServiceManager.Destroy(_mainTopicServiceManager.GetById(id));
            else return RedirectToAction("MainTopicDestroyError", "Error", new {id});

            return RedirectToAction("Create", "MainTopic");
        }
    }
}
