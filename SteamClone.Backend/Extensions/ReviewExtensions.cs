using SteamClone.Backend.DTOs;
using SteamClone.Backend.Entities;
namespace SteamClone.Backend.Extensions;

public static class ReviewExtensions
{
    public static ReviewDto MapToDto(this Reviews review)
    {
        return new ReviewDto
        {
            ReviewId = review.ReviewId,
            GameId = review.GameId,
            Comment = review.Comment,
            Rating = review.Rating,
            ReviewDate = review.ReviewDate
        };
    }
}
