using SteamClone.Backend.Entities;
namespace SteamClone.Backend.Services;

public interface IGameService
{
    IEnumerable<Game> GetAllGames();
    Game? GetById(int id);
    Game CreateGame(Game game);
    Game? UpdateGame(int id, Game updatedGame);
    bool DeleteGame(int id);
}

public class GameService : IGameService
{
    private readonly List<Game> _games = new();

    public GameService()
    {
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
    public IEnumerable<Game> GetAllGames()
    {
        return _games;
    }

    public Game? GetById(int id)
    {
        return _games.FirstOrDefault(game => game.Id == id);
    }

    public Game CreateGame(Game game)
    {
        game.Id = _games.Any() ? _games.Max(g => g.Id) + 1 : 1;
        _games.Add(game);
        return game;
    }

    public Game? UpdateGame(int id, Game updatedGame)
    {
        var existingGame = _games.FirstOrDefault(g => g.Id == id);
        if (existingGame == null) return null;

        if(!string.IsNullOrEmpty(updatedGame.Title))
            existingGame.Title = updatedGame.Title;
        if (!string.IsNullOrEmpty(updatedGame.Description))
            existingGame.Description = updatedGame.Description;
        if (updatedGame.Price >= 0)
            existingGame.Price = updatedGame.Price;
        if (!string.IsNullOrEmpty(updatedGame.Genre))
            existingGame.Genre = updatedGame.Genre;
        if (!string.IsNullOrEmpty(updatedGame.Publisher))
            existingGame.Publisher = updatedGame.Publisher;
        return existingGame;
    }

    public bool DeleteGame(int id)
    {

        var gameToDelete = _games.FirstOrDefault(g => g.Id == id);
        if (gameToDelete == null) return false;
        
        _games.Remove(gameToDelete);
        return true;
    }
}
