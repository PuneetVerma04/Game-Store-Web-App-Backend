using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs.Game;
using AutoMapper;

namespace SteamClone.Backend.Services;

public class GameService : IGameService
{
    private readonly List<Game> _games = new();
    private readonly IMapper _mapper;

    public GameService(IMapper mapper)
    {
        _mapper = mapper;
        _games.AddRange(new[]
        {
            new Game { Id = 1, Title = "Elden Ring", Description = "An action RPG developed by FromSoftware.", Price = 59.99m, Genre = "RPG", Publisher = "Bandai Namco", ReleaseDate = new DateTime(2022, 2, 25), ImageUrl = "https://example.com/eldenring.jpg" },
            new Game { Id = 2, Title = "Cyberpunk 2077", Description = "Open-world RPG set in Night City.", Price = 39.99m, Genre = "Action RPG", Publisher = "CD Projekt", ReleaseDate = new DateTime(2020, 12, 10), ImageUrl = "https://example.com/cyberpunk.jpg" },
            new Game { Id = 3, Title = "God of War Ragnarök", Description = "Action-adventure featuring Kratos and Atreus.", Price = 69.99m, Genre = "Action", Publisher = "Sony Interactive Entertainment", ReleaseDate = new DateTime(2022, 11, 9), ImageUrl = "https://example.com/gowr.jpg" },
            new Game { Id = 4, Title = "The Witcher 3", Description = "Story-driven open-world RPG with Geralt.", Price = 29.99m, Genre = "RPG", Publisher = "CD Projekt", ReleaseDate = new DateTime(2015, 5, 19), ImageUrl = "https://example.com/witcher3.jpg" },
            new Game { Id = 5, Title = "Red Dead Redemption 2", Description = "Open-world western action-adventure.", Price = 49.99m, Genre = "Action Adventure", Publisher = "Rockstar Games", ReleaseDate = new DateTime(2018, 10, 26), ImageUrl = "https://example.com/rdr2.jpg" },
            new Game { Id = 6, Title = "Hades", Description = "Action roguelike dungeon crawler.", Price = 19.99m, Genre = "Roguelike", Publisher = "Supergiant Games", ReleaseDate = new DateTime(2020, 9, 17), ImageUrl = "https://example.com/hades.jpg" },
            new Game { Id = 7, Title = "Minecraft", Description = "Sandbox survival and building game.", Price = 26.95m, Genre = "Sandbox", Publisher = "Mojang", ReleaseDate = new DateTime(2011, 11, 18), ImageUrl = "https://example.com/minecraft.jpg" },
            new Game { Id = 8, Title = "Dark Souls III", Description = "Challenging action RPG in the Souls series.", Price = 39.99m, Genre = "RPG", Publisher = "Bandai Namco", ReleaseDate = new DateTime(2016, 3, 24), ImageUrl = "https://example.com/darksouls3.jpg" },
            new Game { Id = 9, Title = "Grand Theft Auto V", Description = "Open-world crime and adventure game.", Price = 29.99m, Genre = "Action Adventure", Publisher = "Rockstar Games", ReleaseDate = new DateTime(2013, 9, 17), ImageUrl = "https://example.com/gtav.jpg" },
            new Game { Id = 10, Title = "Hollow Knight", Description = "Metroidvania-style action-adventure.", Price = 14.99m, Genre = "Metroidvania", Publisher = "Team Cherry", ReleaseDate = new DateTime(2017, 2, 24), ImageUrl = "https://example.com/hollowknight.jpg" }
        });
    }

    public IEnumerable<GameResponseDTO> GetAllGames()
    {
        return _mapper.Map<IEnumerable<GameResponseDTO>>(_games);
    }

    public GameResponseDTO? GetById(int id)
    {
        var game = _games.FirstOrDefault(game => game.Id == id);
        return game == null ? null : _mapper.Map<GameResponseDTO>(game);
    }

    public GameResponseDTO CreateGame(CreateGameRequestDTO gameDto)
    {
        var game = _mapper.Map<Game>(gameDto);
        game.Id = _games.Any() ? _games.Max(g => g.Id) + 1 : 1;
        _games.Add(game);
        return _mapper.Map<GameResponseDTO>(game);
    }

    public GameResponseDTO? UpdateGame(int id, UpdateGameRequestDTO updatedGameDto)
    {
        var existingGame = _games.FirstOrDefault(g => g.Id == id);
        if (existingGame == null) return null;

        _mapper.Map(updatedGameDto, existingGame);
        return _mapper.Map<GameResponseDTO>(existingGame);
    }

    public bool DeleteGame(int id)
    {
        var gameToDelete = _games.FirstOrDefault(g => g.Id == id);
        if (gameToDelete == null) return false;

        _games.Remove(gameToDelete);
        return true;
    }
}
