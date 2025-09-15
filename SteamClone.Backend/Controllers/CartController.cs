using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.Entities;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
[Authorize (Roles = "Player")]
public class CartController : ControllerBase
{
    public static readonly Dictionary<int, List<CartItem>> cart = new();

    [HttpGet("{userId}")]
    public IActionResult GetCart(int userId)
    {
        if (!cart.ContainsKey(userId))
        {
            return Ok(new List<CartItem>());
        }
        return Ok(cart[userId]);
    }

    [HttpPost("{userId}/add")]
    public IActionResult AddToCart(int userId, [FromBody] CartItem item)
    {
        if (!cart.ContainsKey(userId))
        {
            cart[userId] = new List<CartItem>();
        }
        cart[userId].Add(item);

        return Ok(cart[userId]);
    }

    [HttpDelete("{userId}/remove")]
    public IActionResult RemoveFromCart(int userId, int gameId)
    {
    
        if(!cart.ContainsKey(userId))
        {
            return NotFound("Cart not found");
        }

        var userCart = cart[userId];
        var item = userCart.FirstOrDefault(ci => ci.GameId == gameId);
        if (item == null)
        {
            return NotFound("Game not found in cart");
        }
        userCart.Remove(item);
        return NoContent();
    }
}