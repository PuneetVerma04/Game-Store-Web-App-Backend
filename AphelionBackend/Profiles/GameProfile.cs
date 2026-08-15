using AutoMapper;
using AphelionBackend.Entities;
using AphelionBackend.DTOs.Game;

namespace AphelionBackend.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<Game, GameResponseDTO>()
            .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Username : null))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageUrlResolver>());
        CreateMap<CreateGameRequestDTO, Game>()
            .ForMember(dest => dest.Publisher, opt => opt.Ignore());
        CreateMap<UpdateGameRequestDTO, Game>()
            .ForMember(dest => dest.PublisherId, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
