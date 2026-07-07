namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface ISaleOrderReadService
    {
        Task<int?> GetLastCodeAsync();
        Task<bool> HasOngoingContractForSaleorderAsync(Guid sellerId);
    }
}
