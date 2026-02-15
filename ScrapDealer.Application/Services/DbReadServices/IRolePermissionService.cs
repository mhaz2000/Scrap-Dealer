namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface IRolePermissionService
    {
        Task<IEnumerable<string>> GetRolePermissionsAsync(string userRoleName);
    }
}
