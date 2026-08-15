using AphelionBackend.DTOs.Cart;
using AphelionBackend.Entities;

namespace AphelionBackend.Services;

public interface ICartService
{
    Task<IEnumerable<CartItemDto>> GetCartItemsAsync(int userId);
    Task AddToCartAsync(int userId, int gameId, int quantity);
    Task UpdateCartItemAsync(int userId, int gameId, int quantity);
    Task RemoveCartItemAsync(int userId, int gameId);
    Task ClearCartAsync(int userId);
}