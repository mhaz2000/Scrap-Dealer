using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Notifications;

public record UpdateNotificationCommand(string Title, string Content, Guid Id) : ICommand;
