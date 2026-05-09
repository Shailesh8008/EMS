using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
