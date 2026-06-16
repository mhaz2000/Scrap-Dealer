using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories.Base;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Repositories
{
    public interface IScoreHistoryRepository : IGenericRepository<ScoreHistory>
    {
        Task<List<Score>> GetBuyerScoreHistoriesAsync(Guid id);
        Task<List<Score>> GetSellerScoreHistoriesAsync(Guid id);
    }
}
