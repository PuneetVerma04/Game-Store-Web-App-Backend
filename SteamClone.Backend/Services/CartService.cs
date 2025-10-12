using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs.Cart;
using AutoMapper;

namespace SteamClone.Backend.Services;

public class CartService : ICartService
{
    private readonly List<CartItem> _cartItems = new List<CartItem>();
    private readonly IGameService _gameService;
    private readonly IMapper _mapper;

    public CartService(IGameService gameService, IMapper mapper)
    {
        _gameService = gameService;
        _mapper = mapper;
    }

    public IEnumerable<CartItemDto> GetCartItems(int userId)
    {
        var items = _cartItems.Where(item => item.UserId == userId);
        return _mapper.Map<IEnumerable<CartItemDto>>(items);
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
            var game = _gameService.GetById(gameId);
            if (game == null)
            {
                throw new Exception("Game not found");
            }

            _cartItems.Add(new CartItem
            {
                UserId = userId,
                GameId = gameId,
                Quantity = quantity,
                Game = _mapper.Map<Game>(game)
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
        if (quantity > 0)
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
