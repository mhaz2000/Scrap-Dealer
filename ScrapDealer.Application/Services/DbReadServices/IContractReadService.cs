namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface IContractReadService
    {
        Task<bool> HasOngoingContractAsync(Guid buyerId);
    }
}
