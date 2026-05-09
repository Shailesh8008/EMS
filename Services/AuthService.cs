using EMS.Dto;
using EMS.GenericResponse;
using EMS.IServices;
using EMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EMS.Services
{
    public class AuthService(UserManager<User> _userManager, IConfiguration _configuration, IHttpContextAccessor _contextAccessor) : IAuthService
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
                if (!(await _userManager.CheckPasswordAsync(existingUser, user.Password)))
                {
                    return ServiceResponse<string>.Failure(null, "Invalid Password");
                }
                var token = GetJwtToken(existingUser, await _userManager.GetRolesAsync(existingUser), user.Remember);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = user.Remember ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(1)
                };

                _contextAccessor.HttpContext?.Response.Cookies.Append("token", token, cookieOptions);

                return ServiceResponse<string>.Success(null, "Logged in successfully");
            }
            catch (Exception)
            {
                return ServiceResponse<string>.Failure(null, "Internal server error");
                throw;
            }
        }
        private string GetJwtToken(User user, IList<string> roles, bool save)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    signingCredentials: creds,
                    claims: claims,
                    expires: save ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(1)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
