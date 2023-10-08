using AutoMapper;
using Nasa.BLL.Services.Abstract;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO.User;
using Nasa.Common.Security;
using Nasa.DAL.Context;
using Nasa.BLL.Exceptions;

namespace Nasa.BLL.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        public AuthService(NasaContext context, IMapper mapper, IJwtService jwtFactory, IUserService userService) : base(context, mapper)
        {
            _jwtService = jwtFactory;
            _userService = userService;
        }

        public async Task<AuthorizationResponse> Authorize(LoginUserDto userDto)
        {
            var userEntity = await _userService.GetUserByEmailAsync(userDto.Email);
            if (userEntity == null || !SecurityHelper.ValidatePassword(userDto.Password, userEntity.PasswordHash, userEntity.Salt))
            {
                throw new WrongEmailOrPasswordException();
            }

            return await _jwtService.GetJwt(userEntity);
        }
    }
}