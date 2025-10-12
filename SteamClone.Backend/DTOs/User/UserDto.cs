namespace SteamClone.Backend.DTOs.User;

public class UserDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string? PasswordHash { get; set; }
    public required string Role { get; set; } // e.g., "User", "Admin"
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } // Nullable to allow for users that haven't been updated
}