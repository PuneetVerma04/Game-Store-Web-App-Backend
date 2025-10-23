using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.Services.Interfaces;
using SteamClone.Backend.DTOs.Analytics;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
[Authorize(Roles = "Admin")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet]
    public IActionResult GetSummary()
  {
        var summary = _analyticsService.GetSummary();
 return Ok(summary);
    }

    [HttpGet("topGames")]
    public IActionResult GetTopPurchasedGames([FromQuery] int count = 5)
    {
        var topGames = _analyticsService.GetTopPurchasedGames(count);
        return Ok(topGames);
    }

    [HttpGet("revenue")]
    public IActionResult GetRevenueLast30Days()
    {
        var revenue = _analyticsService.GetRevenueLast30Days();
        return Ok(new { Last30DaysRevenue = revenue });
    }

 [HttpGet("revenue/daily")]
    public IActionResult GetDailyRevenueStats([FromQuery] int days = 30)
    {
        var dailyStats = _analyticsService.GetDailyRevenueStats(days);
        return Ok(dailyStats);
    }
}