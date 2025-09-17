using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.DTOs;
using SteamClone.Backend.Services;
using System.Security.Claims;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
[Authorize (Roles = "Player")]
public class CartController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly ICartService _cartService;
    public CartController(IGameService gameService, ICartService cartService)
    {
        _gameService = gameService;
        _cartService = cartService;
    }
    private int GetUserIdFromClaims()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? throw new Exception("User ID claim missing"));
    }

    [HttpGet]
    public IActionResult GetCart()
    {
        var userId = GetUserIdFromClaims();
        var items = _cartService.GetCartItems(userId)
                                 .Select(ci => new CartItemDto
                                 {
                                     GameId = ci.GameId,
                                     GameTitle = ci.Game.Title,
                                     Quantity = ci.Quantity,
                                     Price = ci.Price,
                                     ImageUrl = ci.ImageUrl
                                 });
      
        return Ok(items);
    }

    [HttpPost("add")]
    public IActionResult AddToCart([FromBody] CartRequest request)
    {
        var userId = GetUserIdFromClaims();
        var game = _gameService.GetById(request.GameId);
        if(game == null)
        {
            return NotFound("Game not found.");
        }
        var items = _cartService.GetCartItems(userId)
                                        .Select(ci => new CartItemDto
                                        {
                                            GameId = ci.GameId,
                                            GameTitle = ci.Game.Title,
                                            Quantity = ci.Quantity,
                                            Price = ci.Price,
                                            ImageUrl = ci.ImageUrl
                                        });
        return Ok(items);
    }


    [HttpPatch("update")]
    public IActionResult UpdateCartItem([FromBody] CartRequest request)
    {
        var userId = GetUserIdFromClaims();
        _cartService.UpdateCartItem(userId, request.GameId, request.Quantity);

        var items = _cartService.GetCartItems(userId)
                                        .Select(ci => new CartItemDto
                                        {
                                            GameId = ci.GameId,
                                            GameTitle = ci.Game.Title,
                                            Quantity = ci.Quantity,
                                            Price = ci.Price,
                                            ImageUrl = ci.ImageUrl
                                        });
        return Ok(items);
    }
}