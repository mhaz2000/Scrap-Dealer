using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Permissions;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;
using ScrapDealer.Shared.SystemPermissions;
using static ScrapDealer.Shared.SystemPermissions.Permissions;

namespace ScrapDealer.Infrastructure.Queries.Handlers.SystemPermissions
{

    internal class GetPermissionsHandler : IQueryHandler<GetPermissionsQuery, PaginatedResult<string>>
    {
        public async Task<PaginatedResult<string>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = Permissions.GetAllPermissions();

            permissions = permissions.Where(t => t.Contains(request.Search ?? string.Empty));

            return await Task.FromResult(permissions.AsQueryable().ToPaginatedResult(request.PageIndex, request.PageSize, request.SortBy ?? string.Empty));
        }
    }
}
