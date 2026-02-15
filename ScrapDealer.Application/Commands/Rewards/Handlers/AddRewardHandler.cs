using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Rewards.Handlers
{
    internal class AddRewardHandler(IRewardFactory factory, IUserRepository userRepository, IRewardRepository rewardRepository) 
        : ICommandHandler<AddRewardCommand>
    {
        public async Task Handle(AddRewardCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(t => t.Id == request.UserId);
            if (user is null)
                throw new BusinessException("کاربر یافت نشد.");

            var reward = factory.Create(request.Amount, request.Description, user);

            await rewardRepository.AddAsync(reward);
        }
    }
}
