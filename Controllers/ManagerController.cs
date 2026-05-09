using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
