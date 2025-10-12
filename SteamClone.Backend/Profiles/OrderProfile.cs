using AutoMapper;
using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs;

namespace SteamClone.Backend.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponseDto>();
            CreateMap<CartItem, OrderItemDto>();
        }
    }
}
