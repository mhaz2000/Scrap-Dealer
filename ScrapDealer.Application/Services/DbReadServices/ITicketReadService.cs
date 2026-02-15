namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface ITicketReadService
    {
        Task<ulong?> GetLastTickerNumberAsync();
    }
}
