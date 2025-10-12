namespace SteamClone.Backend.DTOs.Cart;

public class CartRequest
{
    public int GameId { get; set; }
    public int Quantity { get; set; }
}