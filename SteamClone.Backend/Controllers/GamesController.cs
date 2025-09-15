using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.DTOs;
using SteamClone.Backend.Entities;
using SteamClone.Backend.Extensions;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
public class GamesController : ControllerBase
{
    public static readonly List<Game> games = new List<Game>
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
    };



    [HttpGet]
    [AllowAnonymous]
    public ActionResult<IEnumerable<GameResponseDTO>> GetGames(string? genre = null, decimal? maxPrice = null)
    {
        var result = games.AsEnumerable();

        if (!string.IsNullOrEmpty(genre))
        {
            result = result.Where(g => g.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
        }

        if (maxPrice.HasValue)
        {
            result = result.Where(g => g.Price <= maxPrice.Value);
        }

        return Ok(result.Select(g => g.MapToResponse()));
    }



    [HttpGet("{id}")]
    [AllowAnonymous]
    public ActionResult<GameResponseDTO> GetGameById(int id)
    {
        var game = games.FirstOrDefault(g => g.Id == id);
        if (game == null)
        {
            return NotFound();
        }
        return Ok(game.MapToResponse());
    }



    [HttpPost]
    [Authorize(Roles = "Publisher,Admin")]
    public ActionResult<GameResponseDTO> CreateGame([FromBody] CreateGameRequestDTO newGame)
    {

        var game = new Game
        {
            Id = games.Max(g => g.Id) + 1,
            Title = newGame.Title,
            Description = newGame.Description,
            Price = newGame.Price,
            Genre = newGame.Genre,
            Publisher = newGame.Publisher,
            ReleaseDate = DateTime.UtcNow,
            ImageUrl = "https://example.com/default.jpg"
        };
        games.Add(game);
        var response = game.MapToResponse();
        return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, response);
    }





    [HttpPut("{id}")]
    [Authorize(Roles = "Publisher,Admin")]
    public ActionResult<GameResponseDTO> UpdateGame(int id, [FromBody] UpdateGameRequestDTO updatedGame)
    {
        var game = games.FirstOrDefault(g => g.Id == id);
        if (game == null)
        {
            return NotFound();
        }

        // Update the game properties with the new values
        if(!string.IsNullOrEmpty(updatedGame.Title))
            game.Title = updatedGame.Title;
        if(!string.IsNullOrEmpty(updatedGame.Description))
            game.Description = updatedGame.Description;
        if(updatedGame.Price.HasValue)
            game.Price = updatedGame.Price.Value;
        if(!string.IsNullOrEmpty(updatedGame.Genre))
            game.Genre = updatedGame.Genre;

        return Ok(game.MapToResponse()); //204 Response
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public ActionResult DeleteGame(int id)
    {
        var game = games.FirstOrDefault(g => g.Id == id);
        if (game == null)
        {
            return NotFound(); //404 if NotFound
        }

        games.Remove(game);
        return NoContent(); //204 Response
    }
}