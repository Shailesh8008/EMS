using EMS.Dto;
using EMS.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    public class AuthController(IAuthService _authService) : Controller
    {

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> HandleLogin(LoginUserDto user)
        {
            try
            {
                var result = await _authService.Login(user);
                if (!result.Ok)
                {
                    TempData["Message"] = result.Message;
                    return View("Login");
                }
                TempData["Message"] = result.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["Message"] = "Internal server error";
                return View("Login");
                throw;
            }
        }
    }
}
