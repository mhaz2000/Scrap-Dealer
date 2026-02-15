namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface ISellerReadService
    {
        Task<bool> ExistsByUserIdAsync(Guid userId);
    }
}
