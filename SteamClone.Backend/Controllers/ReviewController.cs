using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamClone.Backend.DTOs.Review;
using SteamClone.Backend.Services;
using AutoMapper;

namespace SteamClone.Backend.Controllers;

[ApiController]
[Route("store/[controller]")]
[Authorize(Roles = "Player,Admin")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ReviewController(IReviewService reviewService, IUserService userService, IMapper mapper)
    {
        _reviewService = reviewService;
        _userService = userService;
        _mapper = mapper;
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
        return Ok(gameReviews);
    }

    [HttpGet("{reviewId}")]
    public ActionResult<ReviewDto> GetReviewById(int reviewId)
    {
        var review = _reviewService.GetReviewById(reviewId);
        if (review == null)
        {
            return NotFound();
        }
        return Ok(review);
    }

    [HttpPost("game/{gameId}/add")]
    [Authorize(Roles = "Player")]
    public ActionResult<ReviewDto> CreateReview(int gameId, [FromBody] ReviewCreateDto newReviewDto)
    {
        var currentUserId = GetCurrentUserId();

        var createdReview = _reviewService.AddReview(gameId, currentUserId, newReviewDto);

        return CreatedAtAction(nameof(GetReviewById), new { reviewId = createdReview.ReviewId }, createdReview);
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
    public ActionResult<ReviewDto> UpdateReview(int reviewId, [FromBody] ReviewCreateDto updatedReviewDto)
    {
        var currentUserId = GetCurrentUserId();
        var updatedReview = _reviewService.UpdateReview(reviewId, currentUserId, updatedReviewDto.Comment, updatedReviewDto.Rating);
        if (updatedReview == null)
        {
            return NotFound();
        }
        return Ok(updatedReview);
    }
}