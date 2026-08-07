namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface IInvoiceReadService
    {
        Task<int?> GetLastCodeAsync();
    }
}
