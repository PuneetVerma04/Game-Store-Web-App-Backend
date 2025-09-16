using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs;
using SteamClone.Backend.Extensions;
using System.Security.Claims;
using SteamClone.Backend.Services;


namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult<IEnumerable<UserDto>> GetUsers([FromQuery] UserRole? role, [FromQuery] string? username)
    {
        var result = _userService.GetAllUsers();
        if (role.HasValue)
        {
            result = result.Where(u => u.Role == role.Value);
        }

        if (!string.IsNullOrEmpty(username))
        {
            result = result.Where(u => u.Username.Contains(username, StringComparison.OrdinalIgnoreCase));
        }
        return Ok(result.Select(u => u.MapToDto()));
    }

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<UserDto> GetUserById(int id)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

        if (currentUserRole != "Admin" && currentUserId != id)
        {
            return Forbid();
        }

        var user = _userService.GetAllUsers().FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        return Ok(user.MapToDto());
    }

    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<UserDto> UpdatedUser(int id, [FromBody] UserDto updatedUser)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

        if (currentUserRole != "Admin" && currentUserId != id)
        {
            return Forbid();
        }
        var user = _userService.GetAllUsers().FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        // Update the user properties with the new values
        user.Username = updatedUser.Username ?? user.Username;
        user.Email = updatedUser.Email ?? user.Email;
        user.UpdatedAt = DateTime.UtcNow; // Update the UpdatedAt field

        return Ok(user.MapToDto()); //204 Response
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public ActionResult DeleteUser(int id)
    {
        var success = _userService.Delete(id);
        if (!success) return NotFound();

        return NoContent();
    }
}