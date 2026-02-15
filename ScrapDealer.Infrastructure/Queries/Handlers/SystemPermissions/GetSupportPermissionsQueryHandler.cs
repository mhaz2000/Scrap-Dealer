using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Queries.Permissions;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.SystemPermissions
{
    internal class GetSupportPermissionsQueryHandler : IQueryHandler<GetSupportPermissionsQuery, PaginatedResult<string>>
    {
        private readonly DbSet<RolePermissionReadModel> _rolePermissions;

        public GetSupportPermissionsQueryHandler(ReadDbContext context)
        {
            _rolePermissions = context.RolePermissions;
        }
        public async Task<PaginatedResult<string>> Handle(GetSupportPermissionsQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _rolePermissions.AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.PermissionName, $"%{query.Search}%"));

            var permissions = dbQuery.Select(t=> t.PermissionName).AsNoTracking();
            var paginatedResult = await permissions.
                ToPaginatedResultAsync(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty);

            return paginatedResult;
        }
    }
}
