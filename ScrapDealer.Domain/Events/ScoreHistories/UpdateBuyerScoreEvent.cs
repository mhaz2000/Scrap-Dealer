using MediatR;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Events.ScoreHistories;

public record UpdateBuyerScoreEvent : INotification
{
    public Guid Id { get; }
    public Score Score { get; set; }

    public UpdateBuyerScoreEvent(Guid id, Score score)
    {
        Id = id; ;
        Score = score;
    }
}
