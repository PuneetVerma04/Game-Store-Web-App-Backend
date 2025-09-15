namespace SteamClone.Backend.Entities;

public enum OrderStatus
{
    Pending,
    Completed,
    Failed,
    Refunded,
    Cancelled
}

public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public List<CartItem> Items { get; set; } = new();
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Completed;
}