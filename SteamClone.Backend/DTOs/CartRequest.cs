namespace SteamClone.Backend.DTOs;

public class CartRequest
{
    public int GameId { get; set; }
    public int Quantity { get; set; } = 1;
}
