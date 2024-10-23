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

        public IActionResult Create(int? selectedMainTopicId)
        {
            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.AltTopics = _altTopicServiceManager.GetAllActives();

            if (selectedMainTopicId != null)
            {
                ViewBag.SelectedMainTopicId = selectedMainTopicId;

                int selectedMainTopicIdToUse = (int)selectedMainTopicId;
                ViewBag.SelectedMainTopic = _mainTopicServiceManager.GetById(selectedMainTopicIdToUse).TopicName;

                ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives().Where(x => x.Id != selectedMainTopicId);
            }

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
                return RedirectToAction("Create", "AltTopic", new { selectedMainTopicId = mainTopic.Id});
            }

            ViewBag.MainTopics = _mainTopicServiceManager.GetAllActives();
            ViewBag.AltTopics = _altTopicServiceManager.GetAllActives();

            return View();
        }

        [HttpPost]
        public IActionResult Recover(int id)
        {
            _altTopicServiceManager.Recover(_altTopicServiceManager.GetById(id));
            return RedirectToAction("FullList", "MainTopic");
        }

        [HttpPost]
        public IActionResult RecoverAll()
        {
            _altTopicServiceManager.RecoverRange(_altTopicServiceManager.GetAllPassives());
            return RedirectToAction("FullList", "MainTopic");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _altTopicServiceManager.Delete(_altTopicServiceManager.GetById(id));
            return RedirectToAction("FullList", "AltTopic");
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            _altTopicServiceManager.DeleteRange(_altTopicServiceManager.GetAllActives());
            return RedirectToAction("FullList", "AltTopic");
        }

        [HttpPost]
        public IActionResult Destroy(int id)
        {
            _altTopicServiceManager.Destroy(_altTopicServiceManager.GetById(id));
            return RedirectToAction("FullList", "AltTopic");
        }

        [HttpPost]
        public IActionResult DestroyAll()
        {
            _altTopicServiceManager.DestroyRange(_altTopicServiceManager.GetAllPassives());
            return RedirectToAction("FullList", "AltTopic");
        }
    }
}
