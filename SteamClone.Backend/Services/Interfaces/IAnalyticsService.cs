using SteamClone.Backend.DTOs.Analytics;

namespace SteamClone.Backend.Services.Interfaces
{
    public interface IAnalyticsService
    {
        AnalyticsSummaryDto GetSummary();
        IEnumerable<TopGameDto> GetTopPurchasedGames(int count = 5);
        decimal GetRevenueLast30Days();
        IEnumerable<RevenueStatsDto> GetDailyRevenueStats(int days = 30);
    }
}
