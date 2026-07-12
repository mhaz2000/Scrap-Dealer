using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Notifications;
public record GetNotificationQuery(Guid Id, Guid UserId, string? UserRole) : IQuery<NotificationDto>;
