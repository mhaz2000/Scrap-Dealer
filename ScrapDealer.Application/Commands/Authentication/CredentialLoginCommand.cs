using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Authentication
{
    //public record CredentialLoginCommand(string Username, string Password, string CaptchaCode, Guid CaptchaId) : ICommand<string>;
    public record CredentialLoginCommand(string Username, string Password) : ICommand<PanelAuthenticationDto>;
}
