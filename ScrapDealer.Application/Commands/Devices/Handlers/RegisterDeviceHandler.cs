using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Devices.Handlers;

public class RegisterDeviceHandler(IUserDeviceTokenFactory factory, IUserDeviceTokenRepository repository)
    : ICommandHandler<RegisterDeviceCommand>
{
    public async Task Handle(RegisterDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = factory.Create(request.UserId, request.Token, request.Provider, request.Platform);
        await repository.RegisterOrActivateAsync(device, cancellationToken);
    }
}
