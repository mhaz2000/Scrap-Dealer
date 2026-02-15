using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Queries.Buyers;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Buyers
{
    internal class GetBuyerStateHandler : IQueryHandler<GetBuyerStateQuery, bool>
    {
        private readonly DbSet<BuyerReadModel> _buyers;

        public GetBuyerStateHandler(ReadDbContext context)
            => _buyers = context.Buyers;

        public async Task<bool> Handle(GetBuyerStateQuery request, CancellationToken cancellationToken)
            => await _buyers.AnyAsync(b => b.UserId == request.UserId);
    }
}
