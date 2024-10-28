using BilgeCollege.BLL.Services.Abstracts;
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
        private readonly I_AltTopicServiceManager _altTopicServiceManager;
        private readonly I_TeacherServiceManager _teacherServiceManager;
        private readonly I_ClassHourServiceManager _classHourServiceManager;

        public MainTopicController(I_MainTopicServiceManager mainTopicServiceManager,I_AltTopicServiceManager altTopicServiceManager, I_TeacherServiceManager teacherServiceManager, I_ClassHourServiceManager classHourServiceManager)
        {
            _mainTopicServiceManager = mainTopicServiceManager;
            _altTopicServiceManager = altTopicServiceManager;
            _teacherServiceManager = teacherServiceManager;
            _classHourServiceManager = classHourServiceManager;
        }

        public IActionResult FullList()
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
        public IActionResult Create(MainTopicVM mainTopicVM)
        {
            if (ModelState.IsValid)
            {
                if (mainTopicVM != null)
                {
                    _mainTopicServiceManager.Create(_mainTopicServiceManager.SetMainTopic(mainTopicVM.TopicName));
                }

                return RedirectToAction("Create", "MainTopic");
            }

            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            return View(mainTopicVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _mainTopicServiceManager.HandleRelationsOnDelete(_altTopicServiceManager, _altTopicServiceManager.GetAllPassives(), id, _teacherServiceManager, _classHourServiceManager);
            _mainTopicServiceManager.Delete(_mainTopicServiceManager.GetById(id));

            return RedirectToAction("FullList", "MainTopic");
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            var classHours = _classHourServiceManager.GetAll();
            foreach(var item in classHours)
            {
                item.AltTopicId = 1; // NONE
            }

            _altTopicServiceManager.DeleteRange(_altTopicServiceManager.GetAllActives());
            _mainTopicServiceManager.DeleteRange(_mainTopicServiceManager.GetAllActives());
            return RedirectToAction("FullList", "MainTopic");
        }

        [HttpPost]
        public IActionResult Destroy(int id)
        {
            _mainTopicServiceManager.HandleRelationsOnDestroy(_altTopicServiceManager, _altTopicServiceManager.GetAllPassives(), id, _teacherServiceManager);
            _mainTopicServiceManager.Destroy(_mainTopicServiceManager.GetById(id));

            return RedirectToAction("FullList", "MainTopic");
        }

        [HttpPost]
        public IActionResult DestroyAll()
        {
            var passiveMainTopics = _mainTopicServiceManager.GetAllPassives();
            foreach (var item in passiveMainTopics)
            {
                _mainTopicServiceManager.HandleRelationsOnDestroy(_altTopicServiceManager, _altTopicServiceManager.GetAllPassives(), item.Id, _teacherServiceManager);
            }

            _mainTopicServiceManager.DestroyRange(_mainTopicServiceManager.GetAllPassives());
            return RedirectToAction("FullList", "MainTopic");
        }

        [HttpPost]
        public IActionResult Recover(int id)
        {
            _mainTopicServiceManager.Recover(_mainTopicServiceManager.GetById(id));
            return RedirectToAction("FullList", "MainTopic");
        }

        [HttpPost]
        public IActionResult RecoverAll()
        {
            _mainTopicServiceManager.RecoverRange(_mainTopicServiceManager.GetAllPassives());
            return RedirectToAction("FullList", "MainTopic");
        }

        public IActionResult Update(int id)
        {
            var mainTopic = _mainTopicServiceManager.GetById(id);
            MainTopicVM mainTopicVM = new MainTopicVM
            {
                Id = mainTopic.Id,
                TopicName = mainTopic.TopicName
            };
            return View(mainTopicVM);
        }

        [HttpPost]
        public IActionResult Update(MainTopicVM mainTopicVM)
        {
            if(mainTopicVM != null)
            {
                var mainTopic = _mainTopicServiceManager.GetById(mainTopicVM.Id);
                var relatedAltTopics = _altTopicServiceManager.GetAll().Where(x => x.MainTopicId == mainTopic.Id).ToList();

                var mainTopics = _mainTopicServiceManager.GetAll();
                mainTopics.Remove(mainTopic);

                if (mainTopics.Where(x => x.TopicName == mainTopicVM.TopicName).Count() == 0)
                {
                    mainTopic.TopicName = mainTopicVM.TopicName;
                    _mainTopicServiceManager.Update(mainTopic);

                    _mainTopicServiceManager.HandleRelationOnUpdate(_altTopicServiceManager, relatedAltTopics, mainTopic);

                    return RedirectToAction("FullList", "MainTopic");
                }
                else
                {
                    ViewData["Error"] = "There already is a main topic with this name";
                    return View(mainTopicVM);
                }
            }
            return View();
        }
    }
}
