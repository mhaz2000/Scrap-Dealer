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
        private readonly DbSet<SaleOrderRequestReadModel> _requests;
        private readonly IMapper _mapper;

        public GetMySaleOrdersHandler(ReadDbContext context, IMapper mapper)
        {
            _saleOrders = context.SaleOrders;
            _contracts = context.Contracts;
            _requests = context.SaleOrderRequests;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<SaleOrderDto>> Handle(GetMySaleOrdersQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _saleOrders.Include(c => c.Seller).Include(t => t.Items).ThenInclude(t => t.SubCategory)
                .Where(c => c.Seller.UserId == query.userId).AsQueryable();
             
            var contracts = await _contracts.Where(t => dbQuery.Select(s => s.Id).Contains(t.SaleOrderId)).ToListAsync();
            var contactsDictionary = contracts.ToDictionary(t => t.SaleOrderId, t => new { t.Id, t.Status });

            var salreOrderRequests = await _requests.Where(t => dbQuery.Select(s => s.Id).Contains(t.SaleOrderId)).Include(t=> t.Buyer).ToListAsync();
            var salreOrderRequestsDictionary = salreOrderRequests.ToDictionary(t => t.SaleOrderId, t => new { t.Id, BuyerName = t.Buyer.FirstName + " " + t.Buyer.LastName });

            var saleOrders = dbQuery.AsNoTracking();
            var paginatedResult = await saleOrders.
                ToPaginatedResultAsync<SaleOrderReadModel, SaleOrderDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            foreach (var item in paginatedResult.Data)
            {
                var contract = contactsDictionary.GetValueOrDefault(item.Id);
                if (contract is not null)
                {
                    item.ContractId = contract.Id;
                    item.HasFinishedOrOngoingContract = !(contract.Status == ContractStatus.CancelledByBuyer || contract.Status == ContractStatus.CancelledBySeller);
                }

                var request = salreOrderRequestsDictionary.GetValueOrDefault(item.Id);
                if (request is not null)
                {
                    item.RequestId = request.Id;
                    item.SendRequestTo = request.BuyerName;
                }
            }

            return paginatedResult;
        }
    }
}
