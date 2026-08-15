using AutoMapper;
using AphelionBackend.Entities;
using AphelionBackend.DTOs.Order;

namespace AphelionBackend.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Game.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Game.ImageUrl));
        }
    }
}
