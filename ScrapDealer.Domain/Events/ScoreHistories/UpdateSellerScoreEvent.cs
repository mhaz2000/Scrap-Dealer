using MediatR;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Events.ScoreHistories;

public record UpdateSellerScoreEvent : INotification
{
    public Score Score { get; set; }
    public Guid Id { get; }

    public UpdateSellerScoreEvent(Guid id, Score score)
    {
        Id = id; ;
        Score = score;
    }
}
