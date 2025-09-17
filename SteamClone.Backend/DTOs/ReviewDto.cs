namespace SteamClone.Backend.DTOs;

public class ReviewDto
{
    public int ReviewId { get; set; }
    public int GameId { get; set; }
    public string? Comment { get; set; }
    public int Rating { get; set; }
    public DateTime ReviewDate { get; set; }
    public string? Username { get; set; }
}
