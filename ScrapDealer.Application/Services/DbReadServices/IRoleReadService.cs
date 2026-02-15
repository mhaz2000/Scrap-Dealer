namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface IRoleReadService
    {
        Task<string?> GetUserRoleNameAsync(Guid userId);
        Task<Guid> GetRoleIdByNameAsync(string roleName);
    }
}
