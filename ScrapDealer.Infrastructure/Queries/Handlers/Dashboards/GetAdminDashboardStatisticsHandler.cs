using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Dashboards;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Dashboards;

internal class GetAdminDashboardStatisticsHandler : IQueryHandler<GetAdminDashboardStatisticsQuery, AdminDashboardStatisticsDto>
{
    private readonly DbSet<InvoiceReadModel> _invoices;
    private readonly DbSet<UserReadModel> _users;
    private readonly DbSet<BuyerReadModel> _buyers;
    private readonly DbSet<SellerReadModel> _sellers;

    public GetAdminDashboardStatisticsHandler(ReadDbContext dbContext)
    {
        _invoices = dbContext.Invoices;
        _users = dbContext.Users;
        _buyers = dbContext.Buyers;
        _sellers = dbContext.Sellers;
    }

    public async Task<AdminDashboardStatisticsDto> Handle(GetAdminDashboardStatisticsQuery request, CancellationToken cancellationToken)
    {
        var usersCount = await _users.CountAsync();
        var invoices = await _invoices.Select(s=> s.Amount).ToListAsync();
        var buyersCount = await _buyers.Where(t=> t.Verified).CountAsync();
        var sellersCount = await _sellers.Where(t=> t.Verified).CountAsync();

        return new AdminDashboardStatisticsDto()
        {
            InstalledNumber = usersCount,
            ActiveUsers = buyersCount + sellersCount,
            Buyers = buyersCount,
            Sellers = sellersCount,
            Invoices = invoices.Count(),
            TotalInvoicesAmount = invoices.Sum(t=> t)
        };
    }
}
