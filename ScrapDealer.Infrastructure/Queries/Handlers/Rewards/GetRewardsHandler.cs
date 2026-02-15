using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Rewards;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Rewards
{
    internal class GetRewardsHandler : IQueryHandler<GetRewardsQuery, PaginatedResult<RewardDto>>
    {
        private readonly DbSet<RewardReadModel> _rewards;
        private readonly IMapper _mapper;
        public GetRewardsHandler(ReadDbContext context, IMapper mapper)
        {
            _rewards = context.Rewards;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<RewardDto>> Handle(GetRewardsQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _rewards.Include(c => c.User).AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.User.FirstName + " " + u.User.LastName, $"%{query.Search}%"));

            var rewards = dbQuery.AsNoTracking();
            var paginatedResult = await rewards.
                ToPaginatedResultAsync<RewardReadModel, RewardDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
