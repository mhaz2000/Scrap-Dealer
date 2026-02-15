namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface IBuyerReadService
    {
        Task<bool> ExistsByUserIdAsync(Guid userId);
    }
}
