using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Users
{
    public record CreateUserCommand(string Username, string Password, Guid CaptchaId, string CaptchaCode) : ICommand;
}
