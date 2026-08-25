using MediatR;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Events.CategoryPriceHistories;
using ScrapDealer.Domain.Events.ScoreHistories;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapDealer.Application.Events.ScoreHistories;

public class UpdateSellerScoreEventHandler(ISellerRepository sellerRepository, IScoreHistoryRepository scoreHistoryRepository)
    : INotificationHandler<UpdateSellerScoreEvent>
{
    public async Task Handle(UpdateSellerScoreEvent @event, CancellationToken cancellationToken)
    {
        var scoreHistories = await scoreHistoryRepository.GetSellerScoreHistoriesAsync(@event.Id);
        var seller = await sellerRepository.GetAsync(s => s.Id == @event.Id);

        if (seller is null )
            return;

        seller.Score = scoreHistories.Count == 0
            ? 0
            : scoreHistories.Sum(s => s.Value) / scoreHistories.Count;
        await sellerRepository.CommitAsync();

    }
}

