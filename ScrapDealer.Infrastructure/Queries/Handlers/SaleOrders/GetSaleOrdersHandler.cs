using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.SaleOrders;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.SaleOrders
{
    internal class GetSaleOrdersHandler : IQueryHandler<GetSaleOrdersQuery, PaginatedResult<SaleOrderDto>>
    {
        private readonly DbSet<SaleOrderReadModel> _saleOrders;
        private readonly IMapper _mapper;

        public GetSaleOrdersHandler(ReadDbContext context, IMapper mapper)
        {
            _saleOrders = context.SaleOrders;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<SaleOrderDto>> Handle(GetSaleOrdersQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _saleOrders.Include(c => c.Seller).Include(t => t.Items).ThenInclude(t => t.SubCategory).AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Seller.FirstName + " " + u.Seller.LastName, $"%{query.Search}%"));

            var saleOrders = dbQuery.AsNoTracking();
            var paginatedResult = await saleOrders.
                ToPaginatedResultAsync<SaleOrderReadModel, SaleOrderDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
