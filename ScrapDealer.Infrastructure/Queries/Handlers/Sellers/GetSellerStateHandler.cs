using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Queries.Sellers;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Sellers
{
    internal class GetSellerStateHandler : IQueryHandler<GetSellerStateQuery, bool>
    {
        private readonly DbSet<SellerReadModel> _sellers;

        public GetSellerStateHandler(ReadDbContext context)
            => _sellers = context.Sellers;

        public async Task<bool> Handle(GetSellerStateQuery request, CancellationToken cancellationToken)
            => await _sellers.AnyAsync(b => b.UserId == request.UserId);
    }
}
