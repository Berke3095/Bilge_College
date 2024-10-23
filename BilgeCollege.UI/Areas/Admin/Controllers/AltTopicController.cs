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
            ViewBag.MainTopics = _mainTopicServiceManager.GetAll();
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
            int idOfMain = (int)_altTopicServiceManager.GetById(id).MainTopicId;

            if (_mainTopicServiceManager.GetById(idOfMain).State == MODELS.Enums.StateEnum.Active) _altTopicServiceManager.Recover(_altTopicServiceManager.GetById(id));

            return RedirectToAction("FullList", "AltTopic");
        }

        [HttpPost]
        public IActionResult RecoverAll()
        {
            var passiveAltTopics = _altTopicServiceManager.GetAllPassives();

            List<AltTopic> altTopicsToDelete = new List<AltTopic>();

            foreach (var item in passiveAltTopics)
            {
                int mainTopicId = (int)item.MainTopicId;
                if (_mainTopicServiceManager.GetById(mainTopicId).State == MODELS.Enums.StateEnum.Active)
                {
                    altTopicsToDelete.Add(item);
                }
            }

            _altTopicServiceManager.RecoverRange(altTopicsToDelete);
            return RedirectToAction("FullList", "AltTopic");
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
