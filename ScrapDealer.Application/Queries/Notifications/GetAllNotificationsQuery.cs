using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Notifications;

public record GetAllNotificationsQuery() : PaginationQuery, IQuery<PaginatedResult<NotificationDto>>;


