using AutoMapper;
using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs.User;

namespace SteamClone.Backend.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UpdateUserDto, User>();
    }
}
