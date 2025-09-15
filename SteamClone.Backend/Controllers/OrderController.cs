using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.Entities;

namespace SteamClone.Backend.Controllers;


[ApiController]
[Route("store/[controller]")]
[Authorize(Roles = "Player,Admin")]
public class OrderController : ControllerBase
{
    public static readonly List<Order> orders = new();
    private static int nextOrderId = 1;

    [HttpGet("{userId}")]
    public IActionResult GetOrdersforUser(int userId)
    {   
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

        if (currentUserRole != "Admin" && currentUserId != userId)
        {
            return Forbid();
        }
        var userOrders = orders.Where(o => o.UserId == userId).ToList();
        return Ok(userOrders);
    }

    [HttpPost("{userId}/checkout")]
    public IActionResult Checkout(int userId)
    {   
        if (!CartController.cart.ContainsKey(userId) || CartController.cart[userId].Count == 0)
        {
            return BadRequest("Cart is empty");
        }

        var cartItems = CartController.cart[userId];
        var totalPrice = cartItems.Sum(item => item.Price);

        var order = new Order
        {
            OrderId = nextOrderId++,
            UserId = userId,
            Items = new List<CartItem>(cartItems),
            TotalPrice = totalPrice,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Completed
        };

        orders.Add(order);
        CartController.cart.Remove(userId);

        return Ok(order);
    }

    [HttpGet("details/{orderId}")]
    [Authorize (Roles = "Player,Admin")]
    public IActionResult GetOrderDetails(int orderId)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order == null)
        {
            return NotFound("Order not found");
        }
        return Ok(order);
    }

    [HttpPatch("{orderId}/status")]
    [Authorize (Roles = "Admin")]
    public IActionResult UpdateOrderStatus(int orderId, [FromBody] OrderStatus newStatus)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order == null)
        {
            return NotFound("Order not found");
        }
        if (!Enum.IsDefined(typeof(OrderStatus), newStatus))
        {
            return BadRequest("Invalid order status");
        }

        order.Status = newStatus;
        return Ok(order);
    }
}