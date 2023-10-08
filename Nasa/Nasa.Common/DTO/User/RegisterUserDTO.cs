using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.Common.DTO
{
    public class RegisterUserDto
    {
            public string Username { get; set; } = null!;
            public string Email { get; set; } = null!;

            public string Password { get; set; } = null!;
            public string? Сoordinates { get; set; } = string.Empty;
    }
}
