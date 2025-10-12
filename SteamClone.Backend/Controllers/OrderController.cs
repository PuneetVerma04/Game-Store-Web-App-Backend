using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.DTOs;
using SteamClone.Backend.Entities;
using SteamClone.Backend.Services;
using AutoMapper;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
[Authorize(Roles = "Player,Admin")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public OrderController(IOrderService orderService, ICartService cartService, IMapper mapper)
    {
        _orderService = orderService;
        _cartService = cartService;
        _mapper = mapper;
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
                    ? _orderService.GetAllOrders()
                    : _orderService.GetOrdersForUser(currentUserId);

        return Ok(orders);
    }

    [HttpPost("checkout")]
    public IActionResult Checkout()
    {
        var currentUserId = GetCurrentUserId();
        var cartItems = _cartService.GetCartItems(currentUserId).ToList();

        if (!cartItems.Any())
        {
            return BadRequest("Cart is empty");
        }

        var order = _orderService.CreateOrder(currentUserId, _mapper.Map<List<CartItem>>(cartItems));
        _cartService.ClearCart(currentUserId);
        return Ok(order);
    }

    [HttpGet("{orderId}")]
    [Authorize(Roles = "Player,Admin")]
    public IActionResult GetOrderDetails(int orderId)
    {
        var order = _orderService.GetOrderById(orderId);
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
        return Ok(order);
    }

    [HttpPatch("{orderId}/status")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateOrderStatus(int orderId, [FromBody] OrderStatus newStatus)
    {
        var success = _orderService.UpdateOrderStatus(orderId, newStatus);
        if (!success)
        {
            return NotFound("Order not found");
        }
        return Ok();
    }
}