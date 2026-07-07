namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface ISaleOrderRequestReadService
    {
        Task<bool> HasOngoingSaleOrderRequest(Guid sellerId);

    }
}
