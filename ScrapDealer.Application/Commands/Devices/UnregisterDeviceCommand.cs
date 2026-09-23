using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Devices;

public record UnregisterDeviceCommand(string Token) : ICommand
{
    public Guid UserId { get; init; }
}