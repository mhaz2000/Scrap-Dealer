using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal sealed class TicketReadService(ReadDbContext context) : ITicketReadService
    {
        private readonly DbSet<TicketReadModel> _tickets = context.Tickets;

        public async Task<ulong?> GetLastTickerNumberAsync()
        {
            return (await _tickets.OrderByDescending(t => t.Number).FirstOrDefaultAsync())?.Number;
        }
    }
}
