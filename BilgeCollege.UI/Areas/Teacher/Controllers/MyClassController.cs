using BilgeCollege.BLL.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCollege.UI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class MyClassController : Controller
    {
        private readonly I_ClassroomServiceManager _classroomServiceManager;

        public MyClassController(I_ClassroomServiceManager classroomServiceManager)
        {
            _classroomServiceManager = classroomServiceManager;
        }

        public IActionResult Show(int? id)
        {
            ViewBag.ActiveClassrooms = _classroomServiceManager.GetAllActives();
            if(id != null)
            {
                var classroom = _classroomServiceManager.GetById((int)id);
                return View(classroom);
            }
            return View();
        }
    }
}
