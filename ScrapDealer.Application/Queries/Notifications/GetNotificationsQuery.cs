using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Notifications;

public record GetNotificationsQuery(Guid UserId, string? UserRole) : PaginationQuery, IQuery<PaginatedResult<NotificationDto>>;


