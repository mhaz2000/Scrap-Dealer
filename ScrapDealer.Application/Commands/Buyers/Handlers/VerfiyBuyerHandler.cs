using ScrapDealer.Application.Commands.Notifications;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Buyers.Handlers
{
    internal class VerfiyBuyerHandler : ICommandHandler<VerifyBuyerCommand>
    {
        private readonly IBuyerRepository _repository;
        private readonly IReferralRepository _referralRepository;
        private readonly ICommandDispatcher _commandDispatcher;

        public VerfiyBuyerHandler(IBuyerRepository repository, IReferralRepository referralRepository, ICommandDispatcher commandDispatcher)
        {
            _repository = repository;
            _referralRepository = referralRepository;
            _commandDispatcher = commandDispatcher;
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

            await _commandDispatcher.DispatchAsync(new AddNotificationCommand(
                "نتیجه احراز هویت",
                "اطلاعات شما توسط مدیز سامانه تایید شد.",
                IsPushEnabled: true,
                UserIds: [buyer.UserId]));
        }
    }
}
