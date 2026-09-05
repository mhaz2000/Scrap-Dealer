using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.NewsQueries;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.NewsHandlers;
internal class GetNewsListHandler : IQueryHandler<GetNewsListQuery, PaginatedResult<NewsDto>>
{
    private readonly DbSet<NewsReadModel> _news;
    private readonly IMapper _mapper;

    public GetNewsListHandler(ReadDbContext context, IMapper mapper)
    {
        _news = context.News;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<NewsDto>> Handle(GetNewsListQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _news.AsQueryable();

        if (!string.IsNullOrEmpty(query.Search))
            dbQuery = dbQuery
                .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Title, $"%{query.Search}%"));

        var news = dbQuery.OrderByDescending(t => t.CreatedAt).AsNoTracking();
        var paginatedResult = await news.
            ToPaginatedResultAsync<NewsReadModel, NewsDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

        return paginatedResult;
    }
}

