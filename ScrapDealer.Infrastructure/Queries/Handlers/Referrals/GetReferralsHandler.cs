using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Referrals;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Referrals
{
    internal class GetReferralsHandler : IQueryHandler<GetReferralsQuery, PaginatedResult<ReferralDto>>
    {
        private readonly DbSet<ReferralReadModel> _referrals;
        private readonly IMapper _mapper;

        public GetReferralsHandler(ReadDbContext context, IMapper mapper)
        {
            _referrals = context.Referrals;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ReferralDto>> Handle(GetReferralsQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _referrals
                .Include(r => r.ReferrerUser)
                .Include(r => r.RefereeUser)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
            {
                dbQuery = dbQuery.Where(r =>
                    Microsoft.EntityFrameworkCore.EF.Functions.Like(r.ReferrerUser.FullName, $"%{query.Search}%") ||
                    Microsoft.EntityFrameworkCore.EF.Functions.Like(r.RefereeUser.FullName, $"%{query.Search}%") ||
                    Microsoft.EntityFrameworkCore.EF.Functions.Like(r.Code, $"%{query.Search}%"));
            }

            var paginatedResult = await dbQuery.AsNoTracking()
                .ToPaginatedResultAsync<ReferralReadModel, ReferralDto>(
                    query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
