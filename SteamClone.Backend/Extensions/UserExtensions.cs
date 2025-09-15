using SteamClone.Backend.DTOs;
using SteamClone.Backend.Entities;
namespace SteamClone.Backend.Extensions;


public static class UserExtensions
{
    public static UserDto MapToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}