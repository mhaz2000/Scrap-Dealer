using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Authentication
{
    public record OtpRequestCommand(string Phone) : ICommand;
}
