﻿using AutoMapper;
using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs.Coupon;

namespace SteamClone.Backend.Profiles;

public class CouponsProfile : Profile
{
    public CouponsProfile()
    {
        CreateMap<Coupons, CouponDto>();
        CreateMap<CreateCouponDto, Coupons>();
    }
}