using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.Common.DTO.User
{
    public class UserDto
    {
        public string? Role { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Coordinats { get; set; } = string.Empty;
    }
}
