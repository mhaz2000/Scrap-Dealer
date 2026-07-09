using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Authentication
{
    public record RefreshTokenCommand : ICommand<AuthenticationDto>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
