using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Devices.Handlers;

public class UnregisterDeviceHandler(IUserDeviceTokenRepository repository)
    : ICommandHandler<UnregisterDeviceCommand>
{
    public async Task Handle(UnregisterDeviceCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByTokenAsync(request.Token, cancellationToken);

        if (existing is null)
            return;

        existing.Deactivate();
        await repository.CommitAsync();
    }
}