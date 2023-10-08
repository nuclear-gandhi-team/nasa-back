using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.Common.DTO.User
{
    public class AuthorizationResponse
    {
        public UserDto UserDto { get; set; } = null!;
        public TokenDto TokenDto { get; set;} = null!;
    }
}
