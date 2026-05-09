using EMS.Dto;
using EMS.GenericResponse;

namespace EMS.IServices
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(LoginUserDto user);
    }
}
