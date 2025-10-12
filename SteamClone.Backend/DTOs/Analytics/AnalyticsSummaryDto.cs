namespace SteamClone.Backend.DTOs.Analytics;

public class AnalyticsSummaryDto
{
    public DateTime Date { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalOrders { get; set; }
    public int TotalUsers { get; set; }
}