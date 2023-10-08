using Nasa.DAL.Entities.Common;

namespace Nasa.DAL.Entities;

public class User: BaseEntity<int>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string PasswordHash { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public bool IsGoogleAuth { get; set; }
    public string? Сoordinates { get; set; } = string.Empty;
}