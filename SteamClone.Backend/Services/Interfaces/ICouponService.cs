using SteamClone.Backend.DTOs.Coupon;

namespace SteamClone.Backend.Services;

public interface ICouponService
{
    IEnumerable<CouponDto> GetAllCoupons();
    CouponDto? GetCouponById(int couponId);
    CouponDto CreateCoupon(CreateCouponDto newCouponDto);
    CouponDto? DeactivateCoupon(int couponId);
}