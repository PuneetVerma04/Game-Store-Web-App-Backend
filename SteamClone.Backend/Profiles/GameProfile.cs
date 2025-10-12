using AutoMapper;
using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs.Game;

namespace SteamClone.Backend.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<Game, GameResponseDTO>();
        CreateMap<CreateGameRequestDTO, Game>();
        CreateMap<UpdateGameRequestDTO, Game>();
    }
}
