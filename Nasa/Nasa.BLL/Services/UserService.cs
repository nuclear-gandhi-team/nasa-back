using Nasa.BLL.Services.Abstract;
using Nasa.Common.DTO;
using Nasa.Common.Security;
using Nasa.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.Services
{
    public class UserService : BaseService
    {
        public UserService(MoodiverseDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<UserDTO> CreateUser(RegisterUserDTO userDto)
        {
            var userEntity = _mapper.Map<ApplicationUser>(userDto);
            var salt = SecurityHelper.GetRandomBytes();

            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(userDto.Password, salt);
            userEntity.AvatarId = null;

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(userEntity);
        }
    }
}
