using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Notifications;

public record SeenNotificationCommand(Guid Id, Guid UserId) : ICommand;
