using System.ComponentModel.DataAnnotations;

namespace SteamClone.Backend.DTOs
{
    public class ReviewCreateDto
    {
        public string? Comment { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
