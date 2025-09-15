using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs;
using SteamClone.Backend.Services;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IUserService _userService; // Changed to interface
  private readonly JwtService _jwtService;

  public AuthController(IUserService userService, JwtService jwtService) // Changed to interface
  {
    _userService = userService;
    _jwtService = jwtService;
  }

  [HttpPost("register")]
  public IActionResult Register([FromBody] RegisterDto registerDto)
  {
    if (_userService.GetByEmail(registerDto.Email) is not null)
      return BadRequest("Email already registered");

    if (!Enum.TryParse<UserRole>(registerDto.Role, out var parsedRole))
      return BadRequest("Invalid role");

    var newUser = new User
    {
      Username = registerDto.Username,
      Email = registerDto.Email,
      Role = parsedRole
    };

    var createdUser = _userService.Create(newUser, registerDto.Password);
    if (createdUser == null)
      return StatusCode(500, "User creation failed");

    var token = _jwtService.GenerateToken(createdUser);

    return Ok(new { token });
  }

  [HttpPost("login")]
  public IActionResult Login([FromBody] LoginDto loginDto)
  {
    var user = _userService.GetByEmail(loginDto.Email);

    if (user is null) return Unauthorized("Invalid email");

    if (!_userService.VerifyPassword(user, loginDto.Password))
      return Unauthorized("Invalid password");

    var token = _jwtService.GenerateToken(user);
    return Ok(new AuthResponseDto
    {
      Token = token,
      Username = user.Username,
      Role = user.Role.ToString()
    });
  }
}