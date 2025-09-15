namespace SteamClone.Backend.DTOs;

public class GameResponseDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Genre { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;  // Instead of PublisherId

}