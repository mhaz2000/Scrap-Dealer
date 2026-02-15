using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Authentication
{
    public record OtpLoginCommand(string Phone, string Code) : ICommand<AuthenticationDto>;
}
