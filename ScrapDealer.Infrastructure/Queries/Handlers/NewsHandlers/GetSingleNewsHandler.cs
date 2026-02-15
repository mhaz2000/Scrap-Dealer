using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.NewsQueries;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.NewsHandlers;

internal class GetSingleNewsHandler : IQueryHandler<GetSingleNewsQuery, NewsDto>
{
    private readonly DbSet<NewsReadModel> _news;
    private readonly IMapper _mapper;

    public GetSingleNewsHandler(ReadDbContext context, IMapper mapper)
    {
        _news = context.News;
        _mapper = mapper;
    }

    public async Task<NewsDto> Handle(GetSingleNewsQuery query, CancellationToken cancellationToken)
    {
        var news = await _news.FirstOrDefaultAsync(c => c.Id == query.Id);
        if (news is null)
            throw new BusinessException("خبر یافت نشد.");

        return _mapper.Map<NewsDto>(news);
    }
}

