using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Users
{
    public record ChangePasswordCommand(Guid UserId, string Password, string OldPassword) : ICommand;
}
