using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.DTOs;
using SteamClone.Backend.Entities;
using SteamClone.Backend.Extensions;
using SteamClone.Backend.Services;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<IEnumerable<GameResponseDTO>> GetGames(string? genre = null, decimal? maxPrice = null)
    {
        var games = _gameService.GetAllGames().ToList();
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
        var game = _gameService.GetById(id);
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
            Title = newGame.Title,
            Description = newGame.Description,
            Price = newGame.Price,
            Genre = newGame.Genre,
            Publisher = newGame.Publisher,
            ReleaseDate = DateTime.UtcNow,
            ImageUrl = newGame.ImageUrl
        };
        var createdGame = _gameService.CreateGame(game);
        var response = createdGame.MapToResponse();
        return CreatedAtAction(nameof(GetGameById), new { id = createdGame.Id }, response);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Publisher,Admin")]
    public ActionResult<GameResponseDTO> UpdateGame(int id, [FromBody] UpdateGameRequestDTO updatedGame)
    {
        var game = _gameService.GetById(id);
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
        var updated = _gameService.UpdateGame(id, game); // corrected variable name from 'existing' to 'game'   
        if (updated == null)
            return NotFound();
        return Ok(game.MapToResponse()); //204 Response
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public ActionResult DeleteGame(int id)
    {
        var game = _gameService.GetById(id);
        if (game == null)
        {
            return NotFound(); //404 if NotFound
        }

        _gameService.DeleteGame(id);
        return NoContent(); //204 Response
    }
}