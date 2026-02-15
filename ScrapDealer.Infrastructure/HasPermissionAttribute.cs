using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScrapDealer.Application.Services;
using ScrapDealer.Infrastructure.EF.Contexts;

namespace ScrapDealer.Infrastructure
{
    public class HasPermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _permission;

        public HasPermissionAttribute(string permission)
        {
            _permission = permission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var roleName = user.Claims.FirstOrDefault(c => c.Type.Contains("role"))?.Value;
            if (string.IsNullOrEmpty(roleName))
            {
                context.Result = new ForbidResult();
                return;
            }

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IMemoryCacheService>();
            var cacheKey = $"role_permissions_{roleName}";

            var permissions = await cacheService.GetAsync<HashSet<string>>(cacheKey);
            if (permissions is null)
            {
                var dbContext = context.HttpContext.RequestServices.GetRequiredService<ReadDbContext>();

                permissions = (await dbContext.RolePermissions
                    .Where(rp => rp.Role.Name == roleName)
                    .Select(rp => rp.PermissionName).ToListAsync())
                    .ToHashSet();

                await cacheService.SetAsync(cacheKey, permissions, TimeSpan.FromMinutes(30));
            }

            if (roleName == "Admin")
                return;

            if (!permissions.Contains(_permission))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
