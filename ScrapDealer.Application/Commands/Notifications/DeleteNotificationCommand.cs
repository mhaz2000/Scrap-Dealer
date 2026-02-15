using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Notifications;

public record DeleteNotificationCommand(Guid Id) : ICommand;
