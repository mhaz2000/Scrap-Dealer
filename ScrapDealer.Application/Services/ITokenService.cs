using System.Security.Claims;

namespace ScrapDealer.Application.Services
{
    public interface ITokenService
    {
        (string token, string refreshToken) GenerateToken(string userId, IEnumerable<Claim>? additionalClaims = null);
    }
}
