namespace SteamClone.Backend.DTOs.Coupon;

public class CouponDto
{
    public required string Code { get; set; }
    public required string CouponName { get; set; }
    public required int DiscountPercent { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
}