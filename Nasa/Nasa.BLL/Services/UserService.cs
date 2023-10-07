using AutoMapper;
using Nasa.BLL.Services.Abstract;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO;
using Nasa.Common.DTO.User;
using Nasa.Common.Security;
using Nasa.DAL.Context;
using Nasa.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(NasaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<UserDto> CreateUser(RegisterUserDto userDto)
        {
            var userEntity = _mapper.Map<User>(userDto);
            var salt = SecurityHelper.GetRandomBytes();

            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.PasswordHash = SecurityHelper.HashPassword(userDto.Password, salt);

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(userEntity);
        }
    }
}
