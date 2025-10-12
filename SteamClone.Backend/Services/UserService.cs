using SteamClone.Backend.Entities;
using AutoMapper;
namespace SteamClone.Backend.Services;
using BCrypt.Net;

public class UserService : IUserService
{
    private readonly List<User> _users = new();
    private readonly IMapper _mapper;

    public UserService(IMapper mapper)
    {
        _mapper = mapper;
        _users.AddRange(new[]
        {
            new User { Id = 1, Username = "john_doe", Email = "john@example.com", PasswordHash = BCrypt.HashPassword("password123"), Role = UserRole.Player, CreatedAt = DateTime.UtcNow },
            new User { Id = 2, Username = "jane_doe", Email = "jane@example.com", PasswordHash = BCrypt.HashPassword("password123"), Role = UserRole.Player, CreatedAt = DateTime.UtcNow },
            new User { Id = 3, Username = "alice_smith", Email = "alice@example.com", PasswordHash = BCrypt.HashPassword("password123"), Role = UserRole.Player, CreatedAt = DateTime.UtcNow },
            new User { Id = 4, Username = "bob_jones", Email = "bob@example.com", PasswordHash = BCrypt.HashPassword("password123"), Role = UserRole.Publisher, CreatedAt = DateTime.UtcNow },
            new User { Id = 5, Username = "charlie_brown", Email = "charlie@example.com", PasswordHash = BCrypt.HashPassword("password123"), Role = UserRole.Admin, CreatedAt = DateTime.UtcNow }
        });
    }

    public User? GetByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public User Create(User user, string rawPassword)
    {
        user.Id = _users.Count + 1;
        user.CreatedAt = DateTime.UtcNow;
        user.PasswordHash = BCrypt.HashPassword(rawPassword);
        _users.Add(user);
        return user;
    }

    public bool VerifyPassword(User user, string password)
    {
        return BCrypt.Verify(password, user.PasswordHash);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _users;
    }

    public bool Delete(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null) return false;

        _users.Remove(user);
        return true;
    }
}