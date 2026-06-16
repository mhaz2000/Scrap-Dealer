using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IScoreHistoryFactory
    {
        ScoreHistory Create(Score score, Buyer buyer, Seller seller, Contract contract, ScoreFor scoreFor, string? comment);
    }
}
