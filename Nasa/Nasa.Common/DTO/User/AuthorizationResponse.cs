using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.Common.DTO.User
{
    public class AuthorizationResponse
    {
        public string? Role { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Coordinats { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
    }
}
