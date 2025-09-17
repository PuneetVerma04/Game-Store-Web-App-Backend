namespace SteamClone.Backend.DTOs;

public class CartItemDto
{
    public int GameId { get; set; }
    public string? GameTitle { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }
}
