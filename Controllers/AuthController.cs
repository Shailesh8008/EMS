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
                string role = GetRole();
                return RedirectToAction("Index", $"{role}");
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
                string role = GetRole();

                return RedirectToAction("Index", $"{role}");
            }
            catch (Exception)
            {
                TempData["Message"] = "Internal server error";
                return View("Login");
                throw;
            }
        }

        public async Task<IActionResult> HandleLogout()
        {
            try
            {
                var result = await _authService.Logout();
                TempData["Message"] = result.Message;
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                TempData["Message"] = "Internal Server error";
                return RedirectToAction("Login");
                throw;
            }
        }

        private string GetRole()
        {
            string role = "Employee";
            if (User.IsInRole("Admin"))
                role = "Admin";
            else if (User.IsInRole("Manager"))
                role = "Manager";

            return role;
        }
    }
}
