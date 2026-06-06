using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.SaleOrders;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.SaleOrders;
internal class GetNearbyOrdersForBuyersHandler : IQueryHandler<GetNearbyOrdersForBuyersQuery, List<SaleOrderDto>>
{
    private readonly DbSet<SaleOrderReadModel> _saleOrders;
    private readonly DbSet<ContractReadModel> _contracts;
    private readonly DbSet<BuyerReadModel> _buyers;
    private readonly IMapper _mapper;

    public GetNearbyOrdersForBuyersHandler(ReadDbContext context, IMapper mapper)
    {
        _saleOrders = context.SaleOrders;
        _contracts = context.Contracts;
        _buyers = context.Buyers;
        _mapper = mapper;
    }

    public async Task<List<SaleOrderDto>> Handle(GetNearbyOrdersForBuyersQuery request, CancellationToken cancellationToken)
    {
        var buyer = await _buyers.FirstOrDefaultAsync(b => b.UserId == request.buyerId);
        var saleOrders = await _saleOrders
            .Include(s => s.Seller)
            .Include(s => s.Items).ThenInclude(s => s.SubCategory)
            .Where(s => s.Status == SaleOrderStatus.ConfirmedBySystem && !s.SaleAtBuyersLocation).ToListAsync();

        var inBoundryOrders = (buyer.ActivityArea == ActivityArea.Whole ?
            saleOrders :
            saleOrders.Where(s => buyer.ActivityArea == TehranPolygonsAreaHelper.GetActivityAreaFromPolygons(s.Latitude.Value, s.Longitude.Value))).Skip(request.skip).Take(request.take);

        var saleOrdersWithContract = _contracts.Where(c => c.Status != ContractStatus.CancelledByBuyer && c.Status != ContractStatus.CancelledBySeller)
            .Select(t => t.SaleOrderId);

        inBoundryOrders = inBoundryOrders.Where(t => !saleOrdersWithContract.Contains(t.Id));

        return _mapper.Map<List<SaleOrderDto>>(inBoundryOrders.ToList());
    }
}

