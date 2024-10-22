using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.MODELS.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AltTopicController : Controller
    {
        private readonly I_AltTopicServiceManager _altTopicServiceManager;
        private readonly I_MainTopicServiceManager _mainTopicServiceManager;

        public AltTopicController(I_AltTopicServiceManager altTopicServiceManager, I_MainTopicServiceManager mainTopicServiceManager)
        {
            _altTopicServiceManager = altTopicServiceManager;
            _mainTopicServiceManager = mainTopicServiceManager;
        }

        public IActionResult FullList()
        {
            ViewBag.ActiveAltTopics = _altTopicServiceManager.GetAllActives();
            ViewBag.PassiveAltTopics = _altTopicServiceManager.GetAllPassives();
            ViewBag.ActiveMainTopics = _mainTopicServiceManager.GetAllActives();
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.AltTopics = _altTopicServiceManager.GetAllActives();
            return View();
        }

        [HttpPost]
        public IActionResult Create(int id)
        {
            var mainTopic = _mainTopicServiceManager.GetById(id);
            if(mainTopic != null)
            {
                AltTopic altTopic = new AltTopic
                {
                    TopicCode = _altTopicServiceManager.CreateTopicCode(mainTopic),
                    MainTopicId = mainTopic.Id
                };
                _altTopicServiceManager.Create(altTopic);
                return RedirectToAction("Create", "AltTopic");
            }

            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.AltTopics = _altTopicServiceManager.GetAllActives();
            return View();
        }
    }
}
