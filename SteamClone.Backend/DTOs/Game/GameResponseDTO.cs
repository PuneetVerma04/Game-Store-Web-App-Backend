namespace SteamClone.Backend.DTOs.Game;

public class GameResponseDTO
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required string Genre { get; set; }
    public required string Publisher { get; set; }
    public required DateTime ReleaseDate { get; set; }
    public required string ImageUrl { get; set; }
}