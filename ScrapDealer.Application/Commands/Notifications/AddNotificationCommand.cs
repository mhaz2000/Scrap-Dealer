using ScrapDealer.Domain.Consts;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Notifications;
public record AddNotificationCommand(string Title, string Content, List<NotificationTarget> targets) : ICommand;
