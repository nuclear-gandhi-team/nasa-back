using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.DAL.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public bool IsGoogleAuth { get; set; }
        public string? Coordinates { get; set; }
        public int RoleId { get; set; }
    }
}
