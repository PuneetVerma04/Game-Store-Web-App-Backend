using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.Entities;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
[Authorize (Roles = "Player,Admin")]
public class ReviewController : ControllerBase
{
    private static readonly List<Reviews> reviews = new();

    private static int nextReviewId = 1;

    [HttpGet("game/{gameId}")]
    [Authorize (Roles = "Player,Admin,Publisher")]
    public ActionResult<IEnumerable<Reviews>> GetReviewForGame(int gameId)
    {
        var gameReviews = reviews.Where(r => r.GameId == gameId).ToList();
        return Ok(gameReviews);
    }

    [HttpGet("{reviewId}")]
    public ActionResult<Reviews> GetReviewById(int reviewId)
    {
        var review = reviews.FirstOrDefault(r => r.ReviewId == reviewId);
        if (review == null)
        {
            return NotFound();
        }
        return Ok(review);
    }

    [HttpPost("game/{gameId}/add")]
    [Authorize (Roles = "Player")]
    public ActionResult<Reviews> CreateReview(int gameId, [FromBody] Reviews newReview)
    {
        newReview.GameId = gameId;
        newReview.ReviewId = nextReviewId++;
        reviews.Add(newReview);
        return CreatedAtAction(nameof(GetReviewForGame), new { gameId = gameId }, newReview);
    }

    [HttpDelete("{reviewId}")]
    [Authorize]
    public ActionResult DeleteReview(int reviewId)
    {
        var review = reviews.FirstOrDefault(r => r.ReviewId == reviewId);
        if (review == null)
        {
            return NotFound();
        }
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

        if (currentUserRole != "Admin" && review.UserId != currentUserId)
            return Forbid();
        
        reviews.Remove(review);
        return NoContent();
    }
}