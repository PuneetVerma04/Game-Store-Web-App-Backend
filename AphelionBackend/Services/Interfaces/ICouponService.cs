using AphelionBackend.DTOs.Coupon;

namespace AphelionBackend.Services;

public interface ICouponService
{
    Task<IEnumerable<CouponDto>> GetAllCouponsAsync();
    Task<CouponDto?> GetCouponByIdAsync(int couponId);
    Task<CouponDto> CreateCouponAsync(CreateCouponDto newCouponDto);
    Task<CouponDto?> DeactivateCouponAsync(int couponId);
}