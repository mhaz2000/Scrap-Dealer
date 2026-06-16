using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Factories
{
    public class ScoreHistoryFactory : IScoreHistoryFactory
    {
        public ScoreHistory Create(Score score, Buyer buyer, Seller seller, Contract contract, ScoreFor scoreFor, string? comment)
        {
            var scoreValue = Score.Create(score);

            return new ScoreHistory(scoreValue, buyer, seller, contract, scoreFor, comment);
        }
    }
}
