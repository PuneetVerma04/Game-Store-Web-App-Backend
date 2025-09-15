namespace SteamClone.Backend.DTOs;

public class UpdateGameRequestDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? Genre { get; set; }
}