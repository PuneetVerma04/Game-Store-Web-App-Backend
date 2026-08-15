namespace AphelionBackend.DTOs.Review;

public class ReviewCreateDto
{
    public string? Comment { get; set; }
    public required int Rating { get; set; }
}