using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal sealed class RolePermissionService : IRolePermissionService
    {
        private readonly DbSet<RolePermissionReadModel> _rolePermissions;

        public RolePermissionService(ReadDbContext context)
        {
            _rolePermissions = context.RolePermissions;
        }
        public async Task<IEnumerable<string>> GetRolePermissionsAsync(string userRoleName) 
            => await _rolePermissions.Include(t=> t.Role).Where(t=> t.Role.Name == userRoleName).Select(t=> t.PermissionName).ToListAsync();
    }
}
