using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Rewards.Handlers
{
    internal class AddRewardHandler(IRewardFactory factory, IUserRepository userRepository, IRewardRepository rewardRepository, IWalletRepository walletRepository,
        IBuyerRepository buyerRepository, ISellerRepository sellerRepository)
        : ICommandHandler<AddRewardCommand>
    {
        public async Task Handle(AddRewardCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(t => t.Id == request.UserId);
            if (user is null)
                throw new BusinessException("کاربر یافت نشد.");

            var reward = factory.Create(request.Amount, request.Description, user);

            var buyer = await buyerRepository.GetAsync(t => t.UserId == user.Id);
            var seller = await sellerRepository.GetAsync(t => t.UserId == user.Id);

            if (buyer is not null)
            {
                var wallet = await walletRepository.GetAsync(t => t.BuyerId == buyer.Id);
                wallet!.Balance += request.Amount;
            }

            if (seller is not null)
            {
                var wallet = await walletRepository.GetAsync(t => t.SellerId == seller.Id);
                wallet!.Balance += request.Amount;
            }

            await rewardRepository.AddAsync(reward);
            await rewardRepository.CommitAsync();

        }
    }
}
