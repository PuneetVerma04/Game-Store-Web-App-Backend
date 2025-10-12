using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.Services;
namespace SteamClone.Backend.Controllers;


[ApiController]
[Route("store/[controller]")]
[Authorize(Roles = "Admin")]
public class AnalyticsController : ControllerBase
{
    private readonly IGameService _gameService; // added game service
    private readonly IUserService _userService; // added user service
    private readonly IOrderService _orderService; // added order service
    

    public AnalyticsController(IUserService userService, IGameService gameService, IOrderService orderService)
    {
        _gameService = gameService; // initialize game service
        _userService = userService; // initialize user service
        _orderService = orderService; // initialize order service
    }

    [HttpGet]
    public IActionResult GetSummary()
    {
        var totalUsers = _userService.GetAllUsers().Count();
        var totalGames = _gameService.GetAllGames().Count();
        var totalOrders = _orderService.GetAllOrders().Count();

        return Ok(new
        {
            TotalUsers = totalUsers,
            TotalGames = totalGames,
            TotalOrders = totalOrders
        });
    }

    [HttpGet("topGames")]
    public IActionResult GetTopPurchasedGames()
    {
        var orders = _orderService.GetAllOrders();
        var games = _gameService.GetAllGames().ToList();

        var topGames = orders
            .SelectMany(o => o.Items)
            .GroupBy(i => i.GameId)
            .Select(g => new
            {
                GameId = g.Key,
                PurchaseCount = g.Count(),
                GameTitle = games.FirstOrDefault(x => x.Id == g.Key)?.Title ?? "Unknown"
            })
            .OrderByDescending(g => g.PurchaseCount)
            .Take(5)
            .ToList();

        return Ok(topGames);
    }

    [HttpGet("revenue")]
    public IActionResult GetRevenueLast30Days()
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-30);
        var revenue = _orderService.GetAllOrders()
            .Where(o => o.OrderDate >= cutoffDate)
            .Sum(o => o.TotalPrice);

        return Ok(new { Last30DaysRevenue = revenue });
    }
}