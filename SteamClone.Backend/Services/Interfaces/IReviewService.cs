using SteamClone.Backend.DTOs;

namespace SteamClone.Backend.Services;

public interface IReviewService
{
    IEnumerable<ReviewDto> GetReviewForGame(int gameId);
    ReviewDto? GetReviewById(int reviewId);
    ReviewDto AddReview(int gameId, int userId, ReviewCreateDto newReviewDto);
    bool DeleteReview(int reviewId, int currentUserId, string currentUserRole);
    ReviewDto? UpdateReview(int reviewId, int currentUserId, string? comment, int? rating);
}