using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.DTOs;
using SteamClone.Backend.Entities;
using SteamClone.Backend.Extensions;
using SteamClone.Backend.Services;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
[Authorize(Roles = "Player,Admin")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IUserService _userService;

    public ReviewController(IReviewService reviewService, IUserService userService)
    {
        _reviewService = reviewService;
        _userService = userService;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("User ID Claim missing"));
    }

    private string GetCurrentUserRole()
    {
        return User.FindFirst(ClaimTypes.Role)?.Value ?? throw new Exception("User Role Claim missing");
    }

    [HttpGet("game/{gameId}")]
    public ActionResult<IEnumerable<ReviewDto>> GetReviewForGame(int gameId)
    {
        var gameReviews = _reviewService.GetReviewForGame(gameId);
        return Ok(gameReviews.Select(r => r.MapToDto()));
    }

    [HttpGet("{reviewId}")]
    public ActionResult<ReviewDto> GetReviewById(int reviewId)
    {
        var review = _reviewService.GetReviewById(reviewId);
        if (review == null)
        {
            return NotFound();
        }
        return Ok(review.MapToDto());
    }

    [HttpPost("game/{gameId}/add")]
    [Authorize (Roles = "Player")]
    public ActionResult<ReviewDto> CreateReview(int gameId, [FromBody] Reviews newReview)
    {
        var currentUserId = GetCurrentUserId();

        var createdReview = _reviewService.AddReview(gameId, currentUserId, newReview);

        var user = _userService.GetById(currentUserId);
        var reviewDto = createdReview.MapToDto();

        return CreatedAtAction(nameof(GetReviewById), new { reviewId = reviewDto.ReviewId }, reviewDto);
    }

    [HttpDelete("{reviewId}")]
    [Authorize]
    public ActionResult DeleteReview(int reviewId)
    {
        var review = _reviewService.GetReviewById(reviewId);
        if (review == null)
        {
            return NotFound();
        }
        var currentUserId = GetCurrentUserId();
        var currentUserRole = GetCurrentUserRole();

        var success = _reviewService.DeleteReview(reviewId, currentUserId, currentUserRole);
        if (!success)
        {
            return Forbid();
        }
        return NoContent();
    }

    [HttpPatch("{reviewId}/update")]
    [Authorize(Roles = "Player")]
    public ActionResult<ReviewDto> UpdateReview(int reviewId, [FromBody] Reviews updatedReview)
    {
        var currentUserId = GetCurrentUserId();
        var updatedReviewDto = _reviewService.UpdateReview(reviewId, currentUserId, updatedReview.Comment, updatedReview.Rating);
        if (updatedReviewDto == null)
        {
            return NotFound();
        }
        return Ok(updatedReviewDto);
    }
}