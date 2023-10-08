using Nasa.Common.DTO.User;

namespace Nasa.BLL.ServicesContracts
{
    public interface IAuthService
    {
        Task<AuthorizationResponse> Authorize(LoginUserDto userDto);
    }
}
