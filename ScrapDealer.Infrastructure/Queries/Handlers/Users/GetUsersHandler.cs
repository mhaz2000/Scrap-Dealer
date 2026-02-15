using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.ModuleExtensions;

using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Application.Queries.Users;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Users
{
    internal sealed class GetUsersHandler : IQueryHandler<GetUsersQuery, PaginatedResult<UserDto>>
    {
        private readonly DbSet<UserReadModel> _users;
        private readonly IMapper _mapper;

        public GetUsersHandler(ReadDbContext context, IMapper mapper)
        {
            _users = context.Users;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _users.Include(t=>t.UserRoles).ThenInclude(t=>t.Role)
                .Where(c=> c.Username.ToLower() != "admin").AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Username, $"%{query.Search}%"));

            var users = dbQuery.AsNoTracking();
            var paginatedResult = await users.ToPaginatedResultAsync<UserReadModel, UserDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
