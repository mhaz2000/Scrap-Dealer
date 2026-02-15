using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Sellers.Handlers
{
    internal class VerfiySellerHandler : ICommandHandler<VerifySellerCommand>
    {
        private readonly ISellerRepository _repository;

        public VerfiySellerHandler(ISellerRepository repository)
            => _repository = repository;

        public async Task Handle(VerifySellerCommand request, CancellationToken cancellationToken)
        {
            var seller = await _repository.GetAsync(c => c.Id == request.Id);
            if (seller is null)
                throw new BusinessException("فروشنده یافت نشد.");

            seller.SetAsVerified();

            await _repository.UpdateAsync(seller);
        }
    }
    internal class SellerToggleActivationHandler : ICommandHandler<SellerToggleActivationCommand>
    {
        private readonly ISellerRepository _repository;

        public SellerToggleActivationHandler(ISellerRepository repository)
            => _repository = repository;

        public async Task Handle(SellerToggleActivationCommand request, CancellationToken cancellationToken)
        {
            var seller = await _repository.GetAsync(c => c.Id == request.Id);
            if (seller is null)
                throw new BusinessException("فروشنده یافت نشد.");

            seller.SetActivationStatus(request.Status);

            await _repository.UpdateAsync(seller);
        }
    }
}