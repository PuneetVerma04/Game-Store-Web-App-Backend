namespace SteamClone.Backend.DTOs.Review;

public class ReviewDto
{
    public int ReviewId { get; set; }
    public required int UserId { get; set; }
    public required int GameId { get; set; }
    public string? Comment { get; set; }
    public int Rating { get; set; }
    public DateTime ReviewDate { get; set; }
}