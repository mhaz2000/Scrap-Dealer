using ScrapDealer.Domain.Consts;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Devices;

public record RegisterDeviceCommand(string Token, PushProvider Provider, DevicePlatform Platform) : ICommand
{
    public Guid UserId { get; init; }
}