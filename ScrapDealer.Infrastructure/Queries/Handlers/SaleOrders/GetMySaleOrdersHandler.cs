using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.SaleOrders;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.SaleOrders
{

    internal class GetMySaleOrdersHandler : IQueryHandler<GetMySaleOrdersQuery, PaginatedResult<SaleOrderDto>>
    {
        private readonly DbSet<SaleOrderReadModel> _saleOrders;
        private readonly DbSet<ContractReadModel> _contracts;
        private readonly IMapper _mapper;

        public GetMySaleOrdersHandler(ReadDbContext context, IMapper mapper)
        {
            _saleOrders = context.SaleOrders;
            _contracts = context.Contracts;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<SaleOrderDto>> Handle(GetMySaleOrdersQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _saleOrders.Include(c => c.Seller).Include(t => t.Items).ThenInclude(t => t.SubCategory)
                .Where(c => c.Seller.UserId == query.userId).AsQueryable();
             
            var contracts = await _contracts.Where(t => dbQuery.Select(s => s.Id).Contains(t.SaleOrderId)).ToListAsync();
            var contactsDictionary = contracts.ToDictionary(t => t.SaleOrderId, t => new { t.Id, t.Status });

            var saleOrders = dbQuery.AsNoTracking();
            var paginatedResult = await saleOrders.
                ToPaginatedResultAsync<SaleOrderReadModel, SaleOrderDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            foreach (var item in paginatedResult.Data)
            {
                var contract = contactsDictionary.GetValueOrDefault(item.Id);
                if (contract is null)
                    continue;
                item.ContractId = contract.Id;
                item.HasFinishedOrOngoingContract = !(contract.Status == ContractStatus.CancelledByBuyer || contract.Status == ContractStatus.CancelledBySeller);
            }

            return paginatedResult;
        }
    }
}
