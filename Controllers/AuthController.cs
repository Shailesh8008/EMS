using EMS.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult HandleLogin(LoginUserDto user)
        {
            return View("Login");
        }
    }
}
