using AutoMapper;
using AphelionBackend.Entities;
using AphelionBackend.DTOs.Review;

namespace AphelionBackend.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.GameTitle, opt => opt.MapFrom(src => src.Game.Title));
        CreateMap<ReviewCreateDto, Review>();
    }
}
