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
using System.Linq.Dynamic.Core;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Users
{
    internal sealed class GetUsersHandler : IQueryHandler<GetUsersQuery, PaginatedResult<UserDto>>
    {
        private readonly DbSet<UserReadModel> _users;
        private readonly DbSet<BuyerReadModel> _buyers;
        private readonly DbSet<SellerReadModel> _sellers;
        private readonly IMapper _mapper;

        public GetUsersHandler(ReadDbContext context, IMapper mapper)
        {
            _users = context.Users;
            _buyers = context.Buyers;
            _sellers = context.Sellers;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _users.Include(t => t.UserRoles).ThenInclude(t => t.Role)
                .Where(c => c.Username.ToLower() != "admin").AsQueryable();

            List<Guid> ids = new List<Guid>();
            if (query.IsActive)
            {
                ids = _buyers.Select(s => s.UserId).ToList().Union(_sellers.Select(s => s.UserId).ToList()).ToList();
                dbQuery = dbQuery.Where(t => ids.Contains(t.Id));
            }

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Username, $"%{query.Search}%") ||
                                Microsoft.EntityFrameworkCore.EF.Functions.Like(u.FirstName + " " + u.LastName, $"%{query.Search}%"));

            var users = dbQuery.AsNoTracking();
            var paginatedResult = await users.ToPaginatedResultAsync<UserReadModel, UserDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
