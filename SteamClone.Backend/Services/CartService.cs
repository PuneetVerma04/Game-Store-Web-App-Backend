using SteamClone.Backend.Entities;
namespace SteamClone.Backend.Services;

public interface ICartService
{
    IEnumerable<CartItem> GetCartItems(int userId);
    void AddToCart(int userId, int gameId, int quantity);
    void UpdateCartItem(int userId, int gameId, int quantity);
    void RemoveCartItem(int userId, int gameId);
    void ClearCart(int userId);
}

public class CartService: ICartService
{
    private readonly List<CartItem> _cartItems = new List<CartItem>();
    private readonly IGameService _gameService;
    public CartService(IGameService gameService)
    {
        _gameService = gameService;
    }
    
    public IEnumerable<CartItem> GetCartItems(int userId)
    {
        return _cartItems.Where(item => item.UserId == userId);
    }

    public void AddToCart(int userId, int gameId, int quantity)
    {
        
        var existingItem = _cartItems.FirstOrDefault(item => item.UserId == userId && item.GameId == gameId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            _cartItems.Add(new CartItem { 
                UserId = userId, 
                GameId = gameId, 
                Quantity = quantity,
                Game = _gameService.GetById(gameId) ?? throw new Exception("Game not found")
            });
        }
    }

    public void UpdateCartItem(int userId, int gameId, int quantity)
    {
        
        var existingItem = _cartItems.FirstOrDefault(item => item.UserId == userId && item.GameId == gameId);
        if (existingItem == null)
        {
            throw new Exception("Item not found in cart");
        }
        if(quantity > 0)
        {
            existingItem.Quantity = quantity;
        }
        else
        {
            _cartItems.Remove(existingItem);
        }
    }

    public void RemoveCartItem(int userId, int gameId)
    {
        
        var existingItem = _cartItems.FirstOrDefault(item => item.UserId == userId && item.GameId == gameId);
        if (existingItem != null)
        {
            _cartItems.Remove(existingItem);
        }
        
    }

    public void ClearCart(int userId)
    {
        _cartItems.RemoveAll(ci => ci.UserId == userId);
    }
}
