namespace SteamClone.Backend.DTOs.Analytics;

public class TopGameDto
{
    public required string Title { get; set; }
    public int TotalPurchases { get; set; }
    public decimal TotalRevenue { get; set; }
}