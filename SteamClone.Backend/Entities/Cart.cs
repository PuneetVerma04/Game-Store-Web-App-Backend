namespace SteamClone.Backend.Entities;

public class CartItem
{
    public int GameId { get; set; }
    public required string GameTitle { get; set; }
    public required decimal Price { get; set; }

}