using SteamClone.Backend.Entities;

namespace SteamClone.Backend.Services;

public interface IReviewService
{
    IEnumerable<Reviews> GetReviewForGame(int gameId);
    Reviews? GetReviewById(int reviewId);
    Reviews AddReview(int gameId, int userId, Reviews newReview);
    bool DeleteReview(int reviewId, int currentUserId, string currentUserRole);
    Reviews? UpdateReview(int reviewId, int currentUserId, string? comment, int? rating);
}

public class ReviewService : IReviewService
{
    private static readonly List<Reviews> reviews = new();
    private static int nextReviewId = 1;

    public IEnumerable<Reviews> GetReviewForGame(int gameId)
    {

        return reviews.Where(r => r.GameId == gameId);
    }

    public Reviews? GetReviewById(int reviewId)
    {
        return reviews.FirstOrDefault(r => r.ReviewId == reviewId);
    }

    public Reviews AddReview(int gameId, int userId, Reviews newReview)
    {
        newReview.ReviewId = nextReviewId++;
        newReview.GameId = gameId;
        newReview.UserId = userId;
        reviews.Add(newReview);
        return newReview;
    }

    public bool DeleteReview(int reviewId, int currentUserId, string currentUserRole)
    {
        var review = reviews.FirstOrDefault(r => r.ReviewId == reviewId);
        if (review == null)
        {
            return false;
        }
        if (review.UserId != currentUserId && currentUserRole != "Admin")
        {
            return false;
        }
        reviews.Remove(review);
        return true;
    }

    public Reviews? UpdateReview(int reviewId, int currentUserId, string? comment, int? rating)
    {
        var review = reviews.FirstOrDefault(r => r.ReviewId == reviewId);
        if (review == null || review.UserId != currentUserId)
        {
            return null;
        }
        if (comment != null)
        {
            review.Comment = comment;
        }
        if (rating.HasValue && rating.Value >= 1 && rating.Value <= 5)
        {
            review.Rating = rating.Value;
        }
        return review;
    }
}
