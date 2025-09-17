using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.Entities;
using SteamClone.Backend.Services;
using SteamClone.Backend.Extensions;

namespace SteamClone.Backend.Controllers;


[ApiController]
[Route("store/[controller]")]
[Authorize(Roles = "Player,Admin")]
public class OrderController : ControllerBase
{
    private readonly IOrderService orderService;
    private readonly ICartService cartService;

    public OrderController(IOrderService orderService, ICartService cartService)
    {
        this.orderService = orderService;
        this.cartService = cartService;
    }
    
    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("User ID Claim missing"));
    }

    private string GetCurrentUserRole()
    {
        return User.FindFirst(ClaimTypes.Role)?.Value ?? throw new Exception("User Role Claim missing");
    }

    [HttpGet]
    public IActionResult GetOrdersforCurrentUser()
    {   
        var currentUserId = GetCurrentUserId();
        var currentUserRole = GetCurrentUserRole();

        var orders = currentUserRole == "Admin"
                    ? orderService.GetAllOrders()
                    : orderService.GetOrdersForUser(currentUserId);

        return Ok(orders.Select(o => o.MapToDto()));
    }

    [HttpPost("checkout")]
    public IActionResult Checkout()
    {   
        var currentUserId = GetCurrentUserId();
        var cartItems = cartService.GetCartItems(currentUserId).ToList();

        if (!cartItems.Any())
        {
            return BadRequest("Cart is empty");
        }

        var order = orderService.CreateOrder(currentUserId, cartItems);
        cartService.ClearCart(currentUserId);
        return Ok(order.MapToDto());
    }

    [HttpGet("{orderId}")]
    [Authorize (Roles = "Player,Admin")]
    public IActionResult GetOrderDetails(int orderId)
    {
        var order = orderService.GetOrderById(orderId);
        if (order == null)
        {
            return NotFound("Order not found");
        }
        var currentUserId = GetCurrentUserId();
        var currentUserRole = GetCurrentUserRole();
        if (currentUserRole != "Admin" && order.UserId != currentUserId)
        {
            return Forbid();
        }
        return Ok(order.MapToDto());
    }

    [HttpPatch("{orderId}/status")]
    [Authorize (Roles = "Admin")]
    public IActionResult UpdateOrderStatus(int orderId, [FromBody] OrderStatus newStatus)
    {
        var order = orderService.GetOrderById(orderId);
        if (order == null)
        {
            return NotFound("Order not found");
        }

        order.Status = newStatus;
        return Ok(order.MapToDto());
    }
}