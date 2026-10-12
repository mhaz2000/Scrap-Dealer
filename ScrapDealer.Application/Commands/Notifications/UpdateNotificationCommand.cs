using ScrapDealer.Domain.Consts;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Notifications;

public record UpdateNotificationCommand(string Title, string Content, List<NotificationTarget>? targets, Guid Id, bool IsPushEnabled = false, List<Guid>? UserIds = null) : ICommand;
