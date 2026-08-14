using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Commands.Addresses;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.SaleOrders;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.SaleOrders;

internal class GetSaleOrderRequestHandler : IQueryHandler<GetSaleOrderRequestsQuery, List<SaleOrderRequestDto>>
{
    private readonly DbSet<SaleOrderRequestReadModel> _saleOrderRequests;
    private readonly DbSet<ContractReadModel> _contracts;
    private readonly IMapper _mapper;
    private readonly ILocationService _locationService;

    public GetSaleOrderRequestHandler(ReadDbContext context, IMapper mapper, ILocationService locationService)
    {
        _saleOrderRequests = context.SaleOrderRequests;
        _contracts = context.Contracts;
        _mapper = mapper;
        _locationService = locationService;
    }

    public async Task<List<SaleOrderRequestDto>> Handle(GetSaleOrderRequestsQuery request, CancellationToken cancellationToken)
    {
        var requests = _saleOrderRequests.Where(t => t.Buyer.UserId == request.UserId && t.SaleOrder.Status == SaleOrderStatus.ConfirmedBySystem)
            .Include(t=> t.Buyer)
            .Include(t=>t.SaleOrder).ThenInclude(t=>t.Seller)
            .Include(t=>t.SaleOrder).ThenInclude(t=>t.Items).ThenInclude(t=> t.SubCategory).AsQueryable();

        var contracts = _contracts.Where(t=> !(t.Status == ContractStatus.CancelledBySeller || t.Status == ContractStatus.CancelledByBuyer));

        requests = requests.Where(t => !contracts.Any(s => s.SaleOrderId == t.SaleOrderId));

        var data =  _mapper.Map<List<SaleOrderRequestDto>>(requests.ToList());

        foreach (var item in data)
        {
            var location = item.Latitude.HasValue && item.Longitude.HasValue 
                ? await _locationService.GetLocationAsync(new LocationCommand(item.Latitude.Value, item.Longitude.Value)) : null;

            if (location is not null)
                item.Address = location.Neighbourhood;
        }

        return data;
    }
}

