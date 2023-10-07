using Nasa.Common.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.ServicesContracts
{
    public interface IAuthService
    {
        Task<AuthorizationResponse> Authorize(LoginUserDto userDto);
    }
}
