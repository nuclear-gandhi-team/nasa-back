using Nasa.Common.DTO.User;
using Nasa.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nasa.DAL.Entities;

namespace Nasa.BLL.ServicesContracts
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(RegisterUserDto userDto);
        Task<User> GetUserByIdAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
