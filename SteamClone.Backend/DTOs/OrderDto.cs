using SteamClone.Backend.Entities;

namespace SteamClone.Backend.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
}
