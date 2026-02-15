using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Supports;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Supports
{
    internal class GetSupportsHandler(ReadDbContext _context, IMapper _mapper) : IQueryHandler<GetSupportsQuery, PaginatedResult<SupportDto>>
    {
        private readonly DbSet<UserReadModel> _users = _context.Users;

        public async Task<PaginatedResult<SupportDto>> Handle(GetSupportsQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _users.Include(t=> t.UserRoles).ThenInclude(t=> t.Role)
                .Where(u=> u.UserRoles.Any(t=> t.Role.Name == "Support")).AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.FirstName + " " + u.LastName, $"%{query.Search}%") ||
                                Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Username, $"%{query.Search}%") ||
                                Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Phone, $"%{query.Search}%"));

            var supports = dbQuery.AsNoTracking();
            var paginatedResult = await supports.
                ToPaginatedResultAsync<UserReadModel, SupportDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
