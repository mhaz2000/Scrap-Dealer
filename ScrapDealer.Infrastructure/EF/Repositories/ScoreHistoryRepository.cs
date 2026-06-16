using Microsoft.EntityFrameworkCore;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Repositories.Base;

namespace ScrapDealer.Infrastructure.EF.Repositories;

internal class ScoreHistoryRepository : GenericRepository<ScoreHistory>, IScoreHistoryRepository
{
    public ScoreHistoryRepository(WriteDbContext context) : base(context)
    {
    }

    public async Task<List<Score>> GetBuyerScoreHistoriesAsync(Guid id)
        => await _context.ScoreHistories.Where(s => s.ScoreFor == Domain.Consts.ScoreFor.Buyer && s.BuyerId == id).Select(s=>s.Score).AsNoTracking().ToListAsync();


    public async Task<List<Score>> GetSellerScoreHistoriesAsync(Guid id)
        => await _context.ScoreHistories.Where(s => s.ScoreFor == Domain.Consts.ScoreFor.Seller && s.SellerId == id).Select(s => s.Score).AsNoTracking().ToListAsync();
}