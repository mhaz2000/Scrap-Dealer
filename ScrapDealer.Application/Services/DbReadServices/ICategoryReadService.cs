namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface ICategoryReadService
    {
        Task<bool> ExistsByNameAsync(string name, Guid? excludedId = null);
    }
}
