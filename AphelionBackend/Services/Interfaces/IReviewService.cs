using AphelionBackend.DTOs.Review;

namespace AphelionBackend.Services;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetReviewForGameAsync(int gameId);
    Task<ReviewDto?> GetReviewByIdAsync(int reviewId);
    Task<ReviewDto> AddReviewAsync(int gameId, int userId, ReviewCreateDto newReviewDto);
    Task<bool> DeleteReviewAsync(int reviewId, int currentUserId, string currentUserRole);
    Task<ReviewDto?> UpdateReviewAsync(int reviewId, int currentUserId, string? comment, int? rating);
}