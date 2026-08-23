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
        var saleOrder = await _saleOrders.FirstOrDefaultAsync(s => s.Id == request.saleOrderId);
        if (saleOrder is null || !saleOrder.SaleAtBuyersLocation)
            throw new BusinessException("دستور فروشی برای ارسال به خریداران یافت نشد.");

        if (saleOrder.Status != SaleOrderStatus.ConfirmedBySystem)
            throw new BusinessException("دستور فروش در انتظار تایید پشتیبان است.");

        if (_contracts.Any(t => t.SaleOrderId == request.saleOrderId && (t.Status == ContractStatus.CancelledByBuyer || t.Status == ContractStatus.CancelledBySeller)))
            throw new BusinessException("برای این دستور فروش قرارداد جاری وجود دارد.");

        if (saleOrder.Latitude is null || saleOrder.Longitude is null)
            throw new BusinessException("موقعیت جغرافیایی دستور فروش مشخص نشده است.");

        var allVerifiedBuyers = _buyers.Where(t => t.Verified && t.IsFixedLocation && t.Latitude != null && t.Longitude != null).ToList();

        var nearbyBuyers = allVerifiedBuyers
            .Where(b => GeoUtils.GetDistanceKm(
                saleOrder.Latitude.Value, saleOrder.Longitude.Value,
                b.Latitude.Value, b.Longitude.Value) <= request.distance)
            .Skip(request.skip)
            .Take(request.take)
            .ToList();

        return _mapper.Map<List<NearbyBuyerDto>>(nearbyBuyers);
    }
}

