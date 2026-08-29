using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.SaleOrders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.SaleOrders
{
    internal class GetSaleOrderHandler : IQueryHandler<GetSaleOrderQuery, SaleOrderDto>
    {
        private readonly DbSet<SaleOrderReadModel> _saleOrders;
        private readonly DbSet<SaleOrderRequestReadModel> _saleOrderRequests;
        private readonly IMapper _mapper;

        public GetSaleOrderHandler(ReadDbContext context, IMapper mapper)
        {
            _saleOrders = context.SaleOrders;
            _saleOrderRequests = context.SaleOrderRequests;
            _mapper = mapper;
        }
        public async Task<SaleOrderDto> Handle(GetSaleOrderQuery query, CancellationToken cancellationToken)
        {
            var saleOrder = await _saleOrders.Include(c => c.Seller).Include(t => t.Items).ThenInclude(t => t.SubCategory).FirstOrDefaultAsync(c => c.Id == query.id);

            var saleOrderRequest = await _saleOrderRequests.Include(t=> t.Buyer).FirstOrDefaultAsync(t => t.SaleOrderId == query.id);

            var data = _mapper.Map<SaleOrderDto>(saleOrder);

            data.SaleOrderRequestSendTo = saleOrderRequest is not null ? saleOrderRequest.Buyer.FirstName + " " + saleOrderRequest.Buyer.LastName :
                string.Empty;

            return data;
        }
    }
}
