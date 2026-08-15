using AutoMapper;
using AphelionBackend.Entities;
using AphelionBackend.DTOs.Cart;

namespace AphelionBackend.Profiles;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Game != null ? src.Game.Title : null))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Game != null ? src.Game.Price : 0))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Game != null ? src.Game.ImageUrl : null));

        CreateMap<CartRequest, CartItem>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Game, opt => opt.Ignore());
    }
}
