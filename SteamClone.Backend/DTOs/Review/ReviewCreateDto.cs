using System.ComponentModel.DataAnnotations;

namespace SteamClone.Backend.DTOs.Review;

public class ReviewCreateDto
{
    public required int UserId { get; set; }
    public required int GameId { get; set; }
    public string? Comment { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }
}