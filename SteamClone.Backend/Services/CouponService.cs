using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs;
using AutoMapper;

namespace SteamClone.Backend.Services;

public class CouponService : ICouponService
{
    private readonly List<Coupons> _coupons = new();
    private readonly IMapper _mapper;

    public CouponService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IEnumerable<CouponDto> GetAllCoupons()
    {
        // Use AutoMapper to map the list of Coupons to CouponDto
        return _mapper.Map<IEnumerable<CouponDto>>(_coupons);
    }

    public CouponDto? GetCouponById(int couponId)
    {
        // Find the coupon and map it to CouponDto
        var coupon = _coupons.FirstOrDefault(c => c.CouponId == couponId);
        return coupon == null ? null : _mapper.Map<CouponDto>(coupon);
    }

    public CouponDto CreateCoupon(CreateCouponDto newCouponDto)
    {
        // Map CreateCouponDto to Coupons entity
        var newCoupon = _mapper.Map<Coupons>(newCouponDto);
        newCoupon.CouponId = _coupons.Any() ? _coupons.Max(c => c.CouponId) + 1 : 1;
        newCoupon.IsActive = true;
        newCoupon.CreatedAt = DateTime.UtcNow;

        // Add the new coupon to the list
        _coupons.Add(newCoupon);

        // Map the created Coupons entity to CouponDto
        return _mapper.Map<CouponDto>(newCoupon);
    }

    public CouponDto? DeactivateCoupon(int couponId)
    {
        // Find the coupon to deactivate
        var coupon = _coupons.FirstOrDefault(c => c.CouponId == couponId);
        if (coupon == null || !coupon.IsActive)
        {
            return null;
        }

        // Update the coupon's status
        coupon.IsActive = false;
        coupon.ExpirationDate = DateTime.UtcNow;

        // Map the updated Coupons entity to CouponDto
        return _mapper.Map<CouponDto>(coupon);
    }
}