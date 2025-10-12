namespace SteamClone.Backend.DTOs.Coupon;

using System.ComponentModel.DataAnnotations;

public class CreateCouponDto
{
    [Required]
    [MaxLength(20)]
    public required string Code { get; set; }

    [Required]
    [MaxLength(50)]
    public required string CouponName { get; set; }

    [Range(0, 100)]
    public required int DiscountPercent { get; set; }

    [Required]
    public required DateTime ExpirationDate { get; set; }
}