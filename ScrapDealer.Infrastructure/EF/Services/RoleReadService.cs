using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{

    internal sealed class RoleReadService : IRoleReadService
    {
        private readonly DbSet<RoleReadModel> _roles;

        public RoleReadService(ReadDbContext context)
            => _roles = context.Roles;

        public async Task<Guid> GetRoleIdByNameAsync(string roleName)
            => (await _roles.FirstOrDefaultAsync(c=> c.Name == roleName))!.Id;

        public async Task<string?> GetUserRoleNameAsync(Guid userId)
            => (await _roles.Include(c=>c.UserRoles).FirstOrDefaultAsync(r => r.UserRoles.Any(ur => ur.UserId == userId)))?.Name;
        

    }
}
