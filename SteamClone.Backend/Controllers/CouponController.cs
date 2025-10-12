using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs;

namespace SteamClone.Backend.Controllers;


[ApiController]
[Route("store/[controller]")]
public class CouponController : ControllerBase
{
    private static readonly List<Coupons> coupons = new();
    private readonly IMapper _mapper;

    public CouponController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Player,Admin")]
    public ActionResult<IEnumerable<CouponDto>> GetCoupons()
    {
        var couponDtos = _mapper.Map<IEnumerable<CouponDto>>(coupons);
        return Ok(couponDtos);
    }

    [HttpGet("{couponId}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<CouponDto> GetCouponById(int couponId)
    {
        var coupon = coupons.FirstOrDefault(c => c.CouponId == couponId);
        if (coupon == null)
        {
            return NotFound();
        }
        var couponDto = _mapper.Map<CouponDto>(coupon);
        return Ok(couponDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult<CouponDto> CreateCoupon([FromBody] CreateCouponDto newCouponDto)
    {
        var newCoupon = _mapper.Map<Coupons>(newCouponDto);
        newCoupon.CouponId = coupons.Any() ? coupons.Max(c => c.CouponId) + 1 : 1;
        newCoupon.IsActive = true;
        newCoupon.CreatedAt = DateTime.UtcNow;

        coupons.Add(newCoupon);

        var createdCouponDto = _mapper.Map<CouponDto>(newCoupon);
        return CreatedAtAction(nameof(GetCouponById), new { couponId = newCoupon.CouponId }, createdCouponDto);
    }

    [HttpPatch("{couponId}/deactivate")]
    [Authorize(Roles = "Admin")]
    public ActionResult<CouponDto> UpdateCoupon(int couponId)
    {
        var updatedCoupon = coupons.FirstOrDefault(c => c.CouponId == couponId);
        if (updatedCoupon == null)
        {
            return NotFound();
        }

        if (!updatedCoupon.IsActive)
        {
            return BadRequest("Coupon is not active");
        }
        updatedCoupon.IsActive = false;
        updatedCoupon.ExpirationDate = DateTime.UtcNow;

        var updatedCouponDto = _mapper.Map<CouponDto>(updatedCoupon);
        return Ok(updatedCouponDto);
    }
}