using SteamClone.Backend.Entities;
using SteamClone.Backend.DTOs.Review;
using AutoMapper;

namespace SteamClone.Backend.Services;

public class ReviewService : IReviewService
{
    private static readonly List<Reviews> reviews = new();
    private static int nextReviewId = 1;
    private readonly IMapper _mapper;

    public ReviewService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IEnumerable<ReviewDto> GetReviewForGame(int gameId)
    {
        var gameReviews = reviews.Where(r => r.GameId == gameId);
        return _mapper.Map<IEnumerable<ReviewDto>>(gameReviews);
    }

    public ReviewDto? GetReviewById(int reviewId)
    {
        var review = reviews.FirstOrDefault(r => r.ReviewId == reviewId);
        return review == null ? null : _mapper.Map<ReviewDto>(review);
    }

    public ReviewDto AddReview(int gameId, int userId, ReviewCreateDto newReviewDto)
    {
        var newReview = _mapper.Map<Reviews>(newReviewDto);
        newReview.ReviewId = nextReviewId++;
        newReview.GameId = gameId;
        newReview.UserId = userId;
        reviews.Add(newReview);
        return _mapper.Map<ReviewDto>(newReview);
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

    public ReviewDto? UpdateReview(int reviewId, int currentUserId, string? comment, int? rating)
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
        return _mapper.Map<ReviewDto>(review);
    }
}
