using ScrapDealer.Application.Commands.Notifications;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Sellers.Handlers
{
    internal class VerfiySellerHandler : ICommandHandler<VerifySellerCommand>
    {
        private readonly ISellerRepository _repository;
        private readonly IReferralRepository _referralRepository;
        private readonly ICommandDispatcher _commandDispatcher;

        public VerfiySellerHandler(ISellerRepository repository, IReferralRepository referralRepository, ICommandDispatcher commandDispatcher)
        {
            _repository = repository;
            _referralRepository = referralRepository;
            _commandDispatcher = commandDispatcher;
        }

        public async Task Handle(VerifySellerCommand request, CancellationToken cancellationToken)
        {
            var seller = await _repository.GetAsync(c => c.Id == request.Id);
            if (seller is null)
                throw new BusinessException("فروشنده یافت نشد.");

            seller.SetAsVerified();

            await _repository.UpdateAsync(seller);

            var referral = await _referralRepository.GetAsync(r => r.RefereeUserId == seller.UserId && r.Status == ReferralStatus.Pending);
            if (referral is not null)
            {
                referral.Approve();
                await _referralRepository.UpdateAsync(referral);
            }


            await _commandDispatcher.DispatchAsync(new AddNotificationCommand(
                "نتیجه احراز هویت",
                "اطلاعات شما توسط مدیز سامانه تایید شد.",
                IsPushEnabled: true,
                UserIds: [seller.UserId]));
        }
    }
}