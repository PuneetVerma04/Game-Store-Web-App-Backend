using SteamClone.Backend.DTOs;
using SteamClone.Backend.Entities;

namespace SteamClone.Backend.Extensions;

public static class GameExtensions
{
    public static GameResponseDTO MapToResponse(this Game game)
    {
        return new GameResponseDTO
        {
            Id = game.Id,
            Title = game.Title,
            Description = game.Description,
            Price = game.Price,
            Genre = game.Genre,
            Publisher = game.Publisher,
        };
    }
}