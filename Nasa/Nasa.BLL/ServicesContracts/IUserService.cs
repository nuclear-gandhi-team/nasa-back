using Nasa.Common.DTO.User;
using Nasa.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.ServicesContracts
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(RegisterUserDto userDto);
    }
}
