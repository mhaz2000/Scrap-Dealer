using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Buyers.Handlers
{
    internal class ToggleActivationHandler : ICommandHandler<BuyerToggleActivationCommand>
    {
        private readonly IBuyerRepository _repository;

        public ToggleActivationHandler(IBuyerRepository repository)
            => _repository = repository;

        public async Task Handle(BuyerToggleActivationCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _repository.GetAsync(c => c.Id == request.Id);
            if (buyer is null)
                throw new BusinessException("خریدار یافت نشد.");

            buyer.SetActivationStatus(request.Status);

            await _repository.UpdateAsync(buyer);
        }
    }
}
