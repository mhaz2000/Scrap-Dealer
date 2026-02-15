using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Notifications;
public record AddNotificationCommand(string Title, string Content) : ICommand;
