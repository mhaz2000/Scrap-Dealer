namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface ISubCategoryReadService
    {
        Task<bool> ExistsByNameAsync(string name, Guid? excludedId = null);
    }
}
