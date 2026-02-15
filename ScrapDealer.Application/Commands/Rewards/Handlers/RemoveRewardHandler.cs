using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Rewards.Handlers
{
    internal class RemoveRewardHandler(IRewardRepository rewardRepository) : ICommandHandler<RemoveRewardCommand>
    {
        public async Task Handle(RemoveRewardCommand request, CancellationToken cancellationToken)
        {
            var reward = await rewardRepository.GetAsync(t => t.Id == request.Id);
            if (reward is null)
                throw new BusinessException("پاداش یافت نشد.");

            await rewardRepository.DeleteAsync(reward.Id);
        }
    }
}
