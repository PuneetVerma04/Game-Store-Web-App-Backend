using AphelionBackend.DTOs.Order;
using AphelionBackend.Entities;

namespace AphelionBackend.Services;

public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(int userId, List<CartItem> cartItems);
    Task<OrderResponseDto?> GetOrderByIdAsync(int id);
    Task<IEnumerable<OrderResponseDto>> GetOrdersForUserAsync(int userId);
    Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
    Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
}