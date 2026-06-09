using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SystemSettings.Handlers
{
    public class UpdateSettingsHandler(ISettingsRepository repository, ISettingsFactory factory) : ICommandHandler<UpdateSettingsCommand>
    {
        public async Task Handle(UpdateSettingsCommand request, CancellationToken cancellationToken)
        {
            var settings = await repository.GetAsync(_=> true);

            if(settings is null)
            {
                settings = factory.Create(request.BuyerCommissionFixedAmount, request.BuyerCommissionRate);
                await repository.AddAsync(settings);
            }
            else
            {
                settings = factory.Update(request.BuyerCommissionFixedAmount, request.BuyerCommissionRate, settings);
                await repository.UpdateAsync(settings);
            }
            await repository.CommitAsync();
        }
    }
}
