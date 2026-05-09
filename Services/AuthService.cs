using EMS.Dto;
using EMS.GenericResponse;
using EMS.IServices;
using EMS.Models;
using Microsoft.AspNetCore.Identity;

namespace EMS.Services
{
    public class AuthService(UserManager<User> _userManager, SignInManager<User> _signInManager) : IAuthService
    {
        public async Task<ServiceResponse<string>> Login(LoginUserDto user)
        {
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    return ServiceResponse<string>.Failure(null, "Invalid Email");
                }

                var result = await _signInManager.PasswordSignInAsync(existingUser, user.Password, isPersistent: user.Remember, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    return ServiceResponse<string>.Failure(null, "Invalid Password");
                }

                return ServiceResponse<string>.Success(null, "Logged in successfully");
            }
            catch (Exception)
            {
                return ServiceResponse<string>.Failure(null, "Internal server error");
                throw;
            }
        }

        public async Task<ServiceResponse<string>> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return ServiceResponse<string>.Success(null, "Logout Success!");
            }
            catch (Exception)
            {
                return ServiceResponse<string>.Failure(null, "Internal server error");
                throw;
            }
        }
    }
}
