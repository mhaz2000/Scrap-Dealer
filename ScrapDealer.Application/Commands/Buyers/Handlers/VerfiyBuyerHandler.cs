using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Buyers.Handlers
{
    internal class VerfiyBuyerHandler : ICommandHandler<VerifyBuyerCommand>
    {
        private readonly IBuyerRepository _repository;

        public VerfiyBuyerHandler(IBuyerRepository repository)
            => _repository = repository;

        public async Task Handle(VerifyBuyerCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _repository.GetAsync(c => c.Id == request.Id);
            if (buyer is null)
                throw new BusinessException("خریدار یافت نشد.");

            buyer.SetAsVerified();

            await _repository.UpdateAsync(buyer);
        }
    }
}
