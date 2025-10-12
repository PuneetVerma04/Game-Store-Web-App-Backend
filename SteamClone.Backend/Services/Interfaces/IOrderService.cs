using SteamClone.Backend.DTOs.Order;
using SteamClone.Backend.Entities;

namespace SteamClone.Backend.Services;

public interface IOrderService
{
    OrderResponseDto CreateOrder(int userId, List<CartItem> cartItems);
    OrderResponseDto? GetOrderById(int id);
    IEnumerable<OrderResponseDto> GetOrdersForUser(int userId);
    IEnumerable<OrderResponseDto> GetAllOrders();
    bool UpdateOrderStatus(int orderId, OrderStatus newStatus);
}