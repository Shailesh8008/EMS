using EMS.Dto;
using EMS.GenericResponse;
using EMS.IServices;
using EMS.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EMS.Services
{
    public class AuthService(UserManager<User> _userManager) : IAuthService
    {
        public async Task<ServiceResponse<string>> Login(LoginUserDto user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser == null)
            {
                return ServiceResponse<string>.Failure(null, "Invalid Email");
            }
            if (!(await _userManager.CheckPasswordAsync(existingUser, user.Password)))
            {
                return ServiceResponse<string>.Failure(null, "Invalid Password");
            }
            var token =
        }
        private string GetJwtToken(User user, List<string> roles)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }
        }
    }
}
