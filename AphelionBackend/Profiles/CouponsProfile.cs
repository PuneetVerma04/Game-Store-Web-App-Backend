using AutoMapper;
using AphelionBackend.Entities;
using AphelionBackend.DTOs.Coupon;

namespace AphelionBackend.Profiles;

public class CouponsProfile : Profile
{
    public CouponsProfile()
    {
        CreateMap<Coupon, CouponDto>();
        CreateMap<CreateCouponDto, Coupon>();
    }
}