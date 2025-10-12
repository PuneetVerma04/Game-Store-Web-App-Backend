namespace SteamClone.Backend.DTOs.Order;

public class OrderDto
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Completed";
}