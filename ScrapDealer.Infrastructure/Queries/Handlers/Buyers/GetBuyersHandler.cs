using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Buyers;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Buyers
{
    internal class GetBuyersHandler : IQueryHandler<GetBuyersQuery, PaginatedResult<BuyerProfileDto>>
    {
        private readonly DbSet<BuyerReadModel> _buyers;
        private readonly IMapper _mapper;
        public GetBuyersHandler(ReadDbContext context, IMapper mapper)
        {
            _buyers = context.Buyers;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<BuyerProfileDto>> Handle(GetBuyersQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _buyers.Include(c => c.User)
                .Where(b => query.Verified.HasValue ? b.Verified == query.Verified : true)
                .Where(b => query.IsWholeSale.HasValue ? b.Verified == query.IsWholeSale : true)
                .Where(b => query.IsFixedLocation.HasValue ? b.IsFixedLocation == query.IsFixedLocation : true).AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.FirstName + " " + u.LastName, $"%{query.Search}%"));

            var buyers = dbQuery.AsNoTracking();
                var paginatedResult = await buyers.
                ToPaginatedResultAsync<BuyerReadModel, BuyerProfileDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
