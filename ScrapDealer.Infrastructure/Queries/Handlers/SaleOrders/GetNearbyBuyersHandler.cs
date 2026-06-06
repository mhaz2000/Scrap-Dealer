using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.SaleOrders;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.SaleOrders;

internal class GetNearbyBuyersHandler : IQueryHandler<GetNearbyBuyersQuery, List<NearbyBuyerDto>>
{
    private readonly DbSet<SaleOrderReadModel> _saleOrders;
    private readonly DbSet<ContractReadModel> _contracts;
    private readonly DbSet<BuyerReadModel> _buyers;
    private readonly IMapper _mapper;

    public GetNearbyBuyersHandler(ReadDbContext context, IMapper mapper)
    {
        _saleOrders = context.SaleOrders;
        _contracts = context.Contracts;
        _buyers = context.Buyers;
        _mapper = mapper;
    }

    public async Task<List<NearbyBuyerDto>> Handle(GetNearbyBuyersQuery request, CancellationToken cancellationToken)
    {
        var seller = await _buyers.FirstOrDefaultAsync(b => b.UserId == request.sellerId);
        var saleOrder = await _saleOrders.FirstOrDefaultAsync(s => s.Id == request.saleOrderId);
        if (saleOrder is null || saleOrder.Status != SaleOrderStatus.ConfirmedBySystem || !saleOrder.SaleAtBuyersLocation)
            throw new BusinessException("دستور فروشی برای ارسال به خریداران یافت نشد.");

        if(_contracts.Any(t=> t.SaleOrderId == request.saleOrderId && (t.Status == ContractStatus.CancelledByBuyer || t.Status == ContractStatus.CancelledBySeller)))
            throw new BusinessException("برای این دستور فروش قرارداد جاری وجود دارد.");

        ActivityArea activityArea = ActivityArea.Whole;
        if (saleOrder.Latitude is not null && saleOrder.Longitude is not null)
            activityArea = TehranPolygonsAreaHelper.GetActivityAreaFromPolygons(saleOrder.Latitude.Value, saleOrder.Longitude.Value);

        var inBoundryBuyers = _buyers.Where(t => t.Verified && (t.ActivityArea == activityArea || t.ActivityArea == ActivityArea.Whole)).Skip(request.skip).Take(request.take);

        return _mapper.Map<List<NearbyBuyerDto>>(inBoundryBuyers.ToList());
    }
}

