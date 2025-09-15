using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.Entities;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
public class CouponController : ControllerBase
{
    private static readonly List<Coupons> coupons = new();

    [HttpGet]
    [Authorize(Roles = "Player,Admin")]
    public ActionResult<IEnumerable<Coupons>> GetCoupons()
    {
        return Ok(coupons);
    }

    [HttpGet("{couponId}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<Coupons> GetCouponById(int couponId)
    {
        var coupon = coupons.FirstOrDefault(c => c.CouponId == couponId);
        if (coupon == null)
        {
            return NotFound();
        }
        return Ok(coupon);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult<Coupons> CreateCoupon([FromBody] Coupons newCoupon)
    {
        newCoupon.CouponId = coupons.Any() ? coupons.Max(c => c.CouponId) + 1 : 1;

        newCoupon.IsActive = true;
        newCoupon.CreatedAt = DateTime.UtcNow;
        newCoupon.ExpirationDate = DateTime.UtcNow.AddDays(30);

        coupons.Add(newCoupon);
        return CreatedAtAction(nameof(GetCoupons), new { id = newCoupon.CouponId }, newCoupon); ;
    }

    [HttpPatch("{couponId}/deactivate")]
    [Authorize(Roles = "Admin")]
    public ActionResult<Coupons> UpdateCoupon(int couponId)
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
        updatedCoupon.ExpirationDate = DateTime.UtcNow; // Example logic to update expiration date+3
        return Ok(updatedCoupon);
    }

}