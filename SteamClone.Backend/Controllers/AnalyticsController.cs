using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.Entities;
namespace SteamClone.Backend.Controllers;


[ApiController]
[Route("store/[controller]")]
[Authorize(Roles = "Admin")]
public class AnalyticsController : ControllerBase
{
    private readonly List<User> user;
    private readonly List<Game> games;
    private readonly List<Order> orders;

    public AnalyticsController()
    {
        user = UsersController.userList;
        games = GamesController.games;
        orders = OrderController.orders;
    }

    [HttpGet]
    public IActionResult GetSummary()
    {
        var totalUsers = user.Count;
        var totalGames = games.Count;
        var totalOrders = orders.Count;

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
        var revenue = orders
            .Where(o => o.OrderDate >= cutoffDate)
            .Sum(o => o.TotalPrice);

        return Ok(new { Last30DaysRevenue = revenue });
    }
}