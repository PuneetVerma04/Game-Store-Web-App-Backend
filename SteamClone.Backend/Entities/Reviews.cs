using System.ComponentModel.DataAnnotations;

namespace SteamClone.Backend.Entities;

public class Reviews
{
    public int ReviewId { get; set; }
    public required int UserId { get; set; }
    public required int GameId { get; set; }
    public string? Comment { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }
    public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
}