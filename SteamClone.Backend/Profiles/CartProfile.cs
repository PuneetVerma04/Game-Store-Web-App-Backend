using AutoMapper;
using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs;

namespace SteamClone.Backend.Profiles;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CartItem, CartItemDto>();
        CreateMap<CartRequest, CartItem>();
    }
}
