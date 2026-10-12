using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Notifications;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;
using System.Linq.Dynamic.Core;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Notifications;

internal class GetNotificationsHandler : IQueryHandler<GetNotificationsQuery, PaginatedResult<NotificationDto>>
{
    private readonly DbSet<NotificationReadModel> _notifications;
    private readonly IMapper _mapper;

    public GetNotificationsHandler(ReadDbContext context, IMapper mapper)
    {
        _notifications = context.Notifications;
        _mapper = mapper;
    }
    public async Task<PaginatedResult<NotificationDto>> Handle(GetNotificationsQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _notifications.AsQueryable();

        if (!string.IsNullOrEmpty(query.Search))
            dbQuery = dbQuery
                .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Title, $"%{query.Search}%"));

        var notifications = await dbQuery.AsNoTracking().ToListAsync(cancellationToken);

        notifications = FilterByAudience(notifications, query.UserRole, query.UserId);

        if (!string.IsNullOrEmpty(query.SortBy))
        {
            var sortParts = query.SortBy.Split('~');
            if (sortParts.Length == 2)
            {
                var field = sortParts[0];
                var direction = sortParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase) ? "descending" : "ascending";
                notifications = notifications.AsQueryable().OrderBy($"{field} {direction}").ToList();
            }
        }

        var totalCount = notifications.Count;
        var pagedItems = notifications
            .Skip(query.PageIndex * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        var dtos = _mapper.Map<List<NotificationDto>>(pagedItems);

        foreach (var item in dtos)
        {
            var notification = pagedItems.First(n => n.Id == item.Id);
            item.Seen = notification.SeenBy.Any(id => id == query.UserId);
        }

        return new PaginatedResult<NotificationDto>(dtos, totalCount, query.PageSize, query.PageIndex);
    }

    private static List<NotificationReadModel> FilterByAudience(List<NotificationReadModel> notifications, string? userRole, Guid userId)
    {
        if (userRole == "Admin")
            return notifications;

        var roleTarget = userRole switch
        {
            "Support" => (Func<NotificationTarget, bool>)(t => t is NotificationTarget.Seller or NotificationTarget.Buyer),
            "Seller" => t => t == NotificationTarget.Seller,
            "Buyer" => t => t == NotificationTarget.Buyer,
            _ => _ => false
        };

        return notifications.Where(n =>
            n.UserIds?.Contains(userId) == true ||
            n.Targets?.Any(roleTarget) == true).ToList();
    }
}
