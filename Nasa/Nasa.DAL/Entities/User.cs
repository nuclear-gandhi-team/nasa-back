using Nasa.DAL.Entities.Common;

namespace Nasa.DAL.Entities;

public class User: BaseEntity<int>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public string? Salt { get; set; }
    public bool IsGoogleAuth { get; set; }
    public string Сoordinates { get; set; } = string.Empty;
    public int RoleId { get; set; }

    public Role Role { get; set; } = null!;
}