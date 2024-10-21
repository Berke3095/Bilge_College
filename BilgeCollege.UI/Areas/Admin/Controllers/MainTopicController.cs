using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.UI.Areas.Admin.Views.Models;
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

        public IActionResult Recycle()
        {
            ViewBag.ActiveMainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.PassiveMainTopics = _mainTopicServiceManager.GetAllPassives();
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            return View();
        }

        [HttpPost]
        public IActionResult Create(MainTopicVM createMainTopicVM)
        {
            if (ModelState.IsValid)
            {
                if (createMainTopicVM != null)
                {
                    MainTopic mainTopic = new MainTopic();

                    if (_mainTopicServiceManager.GetAll().Where(x => x.TopicName == createMainTopicVM.TopicName).ToList().Count() == 0) mainTopic.TopicName = createMainTopicVM.TopicName;
                    else
                    {
                        int i = 1;
                        while(true)
                        {
                            string possibleName = createMainTopicVM.TopicName + i;
                            if (_mainTopicServiceManager.GetAll().Where(x => x.TopicName == possibleName).ToList().Count() == 0)
                            {
                                mainTopic.TopicName = possibleName;
                                break;
                            }
                            else
                            {
                                i++;
                                continue;
                            }
                        }
                    }
                    _mainTopicServiceManager.Create(mainTopic);
                }

                return RedirectToAction("Create", "MainTopic");
            }

            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            return View(createMainTopicVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _mainTopicServiceManager.Delete(_mainTopicServiceManager.GetById(id));
            return RedirectToAction("Recycle", "MainTopic");
        }

        [HttpPost]
        public IActionResult Destroy(int id)
        {
            var owningTeachers = _teacherServiceManager.GetAll().Where(x => x.MainTopicId == id);
            if (owningTeachers.Count() > 0)
            {
                foreach(var item in owningTeachers)
                {
                    item.MainTopicId = null;
                }
            }
            _mainTopicServiceManager.Destroy(_mainTopicServiceManager.GetById(id));
            return RedirectToAction("Recycle", "MainTopic");
        }
    }
}
