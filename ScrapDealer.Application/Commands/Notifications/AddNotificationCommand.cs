using ScrapDealer.Domain.Consts;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Notifications;
public record AddNotificationCommand(string Title, string Content, List<NotificationTarget>? targets = null, bool IsPushEnabled = false, List<Guid>? UserIds = null) : ICommand
{
    public string? UserRole { get; init; } = string.Empty;
}
