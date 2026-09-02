using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Notifications;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;
using System.Linq.Dynamic.Core;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Notifications;

internal class GetAllNotificationsHandler : IQueryHandler<GetAllNotificationsQuery, PaginatedResult<NotificationDto>>
{
    private readonly DbSet<NotificationReadModel> _notifications;
    private readonly IMapper _mapper;

    public GetAllNotificationsHandler(ReadDbContext context, IMapper mapper)
    {
        _notifications = context.Notifications;
        _mapper = mapper;
    }
    public async Task<PaginatedResult<NotificationDto>> Handle(GetAllNotificationsQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _notifications.AsQueryable();

        if (!string.IsNullOrEmpty(query.Search))
            dbQuery = dbQuery
                .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Title, $"%{query.Search}%"));

        var notifications = dbQuery.AsNoTracking();
        var paginatedResult = await notifications.
            ToPaginatedResultAsync<NotificationReadModel, NotificationDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

        return paginatedResult;
    }
}
