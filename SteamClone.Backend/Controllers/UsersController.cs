using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.DTOs;
using SteamClone.Backend.Services;
using AutoMapper;
using System.Security.Claims;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult<IEnumerable<UserDto>> GetUsers([FromQuery] string? username)
    {
        var result = _userService.GetAllUsers();

        if (!string.IsNullOrEmpty(username))
        {
            result = result.Where(u => u.Username.Contains(username, StringComparison.OrdinalIgnoreCase));
        }
        return Ok(result);
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

        var user = _userService.GetById(id);
        if (user == null) return NotFound();

        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<UserDto> UpdatedUser(int id, [FromBody] UpdateUserDto updatedUserDto)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

        if (currentUserRole != "Admin" && currentUserId != id)
        {
            return Forbid();
        }

        var user = _userService.GetById(id);
        if (user == null)
        {
            return NotFound();
        }

        _mapper.Map(updatedUserDto, user);
        return Ok(user);
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