using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Buyers.Handlers
{
    internal class VerfiyBuyerHandler : ICommandHandler<VerifyBuyerCommand>
    {
        private readonly IBuyerRepository _repository;
        private readonly IReferralRepository _referralRepository;

        public VerfiyBuyerHandler(IBuyerRepository repository, IReferralRepository referralRepository)
        {
            _repository = repository;
            _referralRepository = referralRepository;
        }

        public async Task Handle(VerifyBuyerCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _repository.GetAsync(c => c.Id == request.Id);
            if (buyer is null)
                throw new BusinessException("خریدار یافت نشد.");

            buyer.SetAsVerified();

            await _repository.UpdateAsync(buyer);

            var referral = await _referralRepository.GetAsync(r => r.RefereeUserId == buyer.UserId && r.Status == ReferralStatus.Pending);
            if (referral is not null)
            {
                referral.Approve();
                await _referralRepository.UpdateAsync(referral);
            }
        }
    }
}
