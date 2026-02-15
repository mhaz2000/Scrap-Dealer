using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Sellers;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Sellers
{
    internal class GetSellersHandler : IQueryHandler<GetSellersQuery, PaginatedResult<SellerProfileDto>>
    {
        private readonly DbSet<SellerReadModel> _sellers;
        private readonly IMapper _mapper;
        public GetSellersHandler(ReadDbContext context, IMapper mapper)
        {
            _sellers = context.Sellers;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<SellerProfileDto>> Handle(GetSellersQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _sellers.Include(c => c.User).Where(b => query.Verified.HasValue ? b.Verified == query.Verified : true).AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.FirstName + " " + u.LastName, $"%{query.Search}%"));

            var sellers = dbQuery.AsNoTracking();
            var paginatedResult = await sellers.
                ToPaginatedResultAsync<SellerReadModel, SellerProfileDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
