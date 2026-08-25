using MediatR;
using ScrapDealer.Domain.Events.ScoreHistories;
using ScrapDealer.Domain.Repositories;

namespace ScrapDealer.Application.Events.ScoreHistories;

public class UpdateBuyerScoreEventHandler(IBuyerRepository buyerRepository, IScoreHistoryRepository scoreHistoryRepository)
    : INotificationHandler<UpdateBuyerScoreEvent>
{
    public async Task Handle(UpdateBuyerScoreEvent @event, CancellationToken cancellationToken)
    {
        var scoreHistories = await scoreHistoryRepository.GetBuyerScoreHistoriesAsync(@event.Id);
        var buyer = await buyerRepository.GetAsync(s => s.Id == @event.Id);

        if (buyer is null)
            return;

        buyer.Score = scoreHistories.Count == 0
            ? 0
            : scoreHistories.Sum(s => s.Value) / scoreHistories.Count;
        await buyerRepository.CommitAsync();

    }
}

