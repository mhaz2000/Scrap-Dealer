using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Sellers;
using ScrapDealer.Application.Queries.Tickets;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Tickets
{
    internal class GetTicketsHandler : IQueryHandler<GetTicketsQuery, PaginatedResult<TicketDto>>
    {
        private readonly DbSet<TicketReadModel> _tickets;
        private readonly DbSet<UserRoleReadModel> _userRoles;
        private readonly DbSet<RoleReadModel> _roles;
        private readonly IMapper _mapper;
        public GetTicketsHandler(ReadDbContext context, IMapper mapper)
        {
            _tickets = context.Tickets;
            _userRoles = context.UserRoles;
            _roles = context.Roles;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<TicketDto>> Handle(GetTicketsQuery query, CancellationToken cancellationToken)
        {
            var userRoles = _userRoles.Where(t => t.UserId == query.UserId).Select(s=> s.RoleId);
            var roles = _roles.Where(t => userRoles.Contains(t.Id));

            var dbQuery = _tickets.Include(c => c.Messages).ThenInclude(t => t.Sender).AsQueryable();
            if (!roles.Any(t => t.Name.ToLower() == "admin"))
                dbQuery = dbQuery.Where(t => t.Messages.Any(s => s.SenderId == query.UserId));

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Title, $"%{query.Search}%"));

            var sellers = dbQuery.AsNoTracking();
            var paginatedResult = await sellers.
                ToPaginatedResultAsync<TicketReadModel, TicketDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
