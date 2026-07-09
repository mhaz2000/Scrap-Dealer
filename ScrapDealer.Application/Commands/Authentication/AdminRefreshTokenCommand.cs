using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Authentication
{
    public class AdminRefreshTokenCommand : ICommand<PanelAuthenticationDto>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
