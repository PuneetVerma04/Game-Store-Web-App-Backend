namespace SteamClone.Backend.DTOs;

public class OrderItemDto
{
    public int GameId { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }
}
