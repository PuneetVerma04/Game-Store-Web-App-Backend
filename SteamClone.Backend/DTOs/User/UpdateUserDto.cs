namespace SteamClone.Backend.DTOs.User;

public class UpdateUserDto
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string? PasswordHash { get; set; }
    public required string Role { get; set; } // e.g., "User", "Admin"
}