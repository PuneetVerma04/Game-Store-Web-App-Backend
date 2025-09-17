using SteamClone.Backend.Entities;
namespace SteamClone.Backend.Services;

public interface IOrderService
{
    Order CreateOrder(int userId, List<CartItem> cartItems);
    Order? GetOrderById(int id);
    IEnumerable<Order> GetOrdersForUser(int userId);
    IEnumerable<Order> GetAllOrders();
    bool UpdateOrderStatus(int orderId, Order updatedOrder);
}

public class OrderService : IOrderService
{
    private readonly List<Order> _orders = new();

    public Order CreateOrder(int userId, List<CartItem> cartItems) 
    {
        var order = new Order
        {
            OrderId = _orders.Count + 1,
            UserId = userId,
            Items = new List<CartItem>(cartItems),
            TotalPrice = cartItems.Sum(item => item.Price * item.Quantity),
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Completed
        };
        _orders.Add(order);
        return order;
    }

    public Order? GetOrderById(int orderId) 
    {
        return _orders.FirstOrDefault(o => o.OrderId == orderId);
    }
    
    public IEnumerable<Order> GetOrdersForUser(int userId)
    {
        return _orders.Where(o => o.UserId == userId);
    }
    
    public IEnumerable<Order> GetAllOrders() 
    {
        return _orders;
    }

    public bool UpdateOrderStatus(int orderId, Order updatedOrder) 
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order == null) return false;
        order.Status = updatedOrder.Status;
        return true;
    }
}
