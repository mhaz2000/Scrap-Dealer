using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal class InvoiceReadService : IInvoiceReadService
    {
        private readonly DbSet<InvoiceReadModel> _invoices;

        public InvoiceReadService(ReadDbContext context) => _invoices = context.Invoices;

        public async Task<int?> GetLastCodeAsync() => await _invoices.MaxAsync(i => (int?)i.Code);
    }
}
