using AutoMapper;
using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs.Order;

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
