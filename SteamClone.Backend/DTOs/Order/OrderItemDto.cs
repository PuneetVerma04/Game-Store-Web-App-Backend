namespace SteamClone.Backend.DTOs.Order;

public class OrderItemDto
{
    public int GameId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }
}