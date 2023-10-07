using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nasa.BLL.Services.Abstract;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO.User;
using Nasa.Common.Security;
using Nasa.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IJwtService _jwtService;

        public AuthService(NasaContext context, IMapper mapper, IJwtService jwtFactory) : base(context, mapper)
        {
            _jwtService = jwtFactory;
        }

        public async Task<AuthorizationResponse> Authorize(LoginUserDto userDto)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
            if (userEntity == null)
            {
                throw new KeyNotFoundException("User hasn't been found");
            }

            if (!SecurityHelper.ValidatePassword(userDto.Password, userEntity.PasswordHash, userEntity.Salt))
            {
                throw new ArgumentException("Incorrect password");
            }

            return await _jwtService.GetJwt(userEntity);
        }
    }
}