using SteamClone.Backend.DTOs;
using SteamClone.Backend.Entities;

namespace SteamClone.Backend.Services;

public interface ICartService
{
    IEnumerable<CartItemDto> GetCartItems(int userId);
    void AddToCart(int userId, int gameId, int quantity);
    void UpdateCartItem(int userId, int gameId, int quantity);
    void RemoveCartItem(int userId, int gameId);
    void ClearCart(int userId);
}