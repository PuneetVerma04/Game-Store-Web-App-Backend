using AutoMapper;
using AphelionBackend.DTOs.Game;
using AphelionBackend.Entities;

namespace AphelionBackend.Profiles;

public class ImageUrlResolver : IValueResolver<Game, GameResponseDTO, string?>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageUrlResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Resolve(Game source, GameResponseDTO destination, string? destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.ImageUrl))
            return null;

        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null)
            return source.ImageUrl;

        // ImageUrl in DB is "images/filename.jpeg" → build absolute URL
        return $"{request.Scheme}://{request.Host}/{source.ImageUrl}";
    }
}
