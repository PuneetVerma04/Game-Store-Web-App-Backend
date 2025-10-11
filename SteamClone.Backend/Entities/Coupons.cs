namespace SteamClone.Backend.Entities;

public class Coupons
{
    public int CouponId { get; set; }
    public required string Code { get; set; }
    public required string CouponName { get; set; }
    public required int DiscountPercent { get; set; }
    public required bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public required DateTime ExpirationDate { get; set; }
}