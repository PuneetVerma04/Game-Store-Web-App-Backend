using SteamClone.Backend.Entities;
namespace SteamClone.Backend.DTOs;

public class UpdateUserDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
}