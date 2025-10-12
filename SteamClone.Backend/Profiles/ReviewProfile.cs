using AutoMapper;
using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs.Review;

namespace SteamClone.Backend.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Reviews, ReviewDto>();
        CreateMap<ReviewCreateDto, Reviews>();
    }
}
