using ScrapDealer.Application.Services;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Notifications.Handlers
{
    public class AddNotificationHandler(
        INotificationFactory factory,
        INotificationRepository repository,
        IPushNotificationDispatcher pushNotificationDispatcher) : ICommandHandler<AddNotificationCommand>
    {
        public async Task Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.UserRole == "Support" && request.targets?.Any(t => t == NotificationTarget.Support) == true)
                throw new BusinessException("پشتیبان نمی‌تواند برای پشتیبان‌ها اعلان ایجاد کند.");

            var hasTargets = request.targets is { Count: > 0 };
            var hasUsers = request.UserIds is { Count: > 0 };
            if (hasTargets == hasUsers)
                throw new BusinessException("اعلان باید دقیقا یک گروه هدف یا فهرست کاربران داشته باشد.");

            var userIds = request.UserIds?.Distinct().ToList();
            var notification = factory.Create(request.Title, request.Content, request.targets, request.IsPushEnabled, userIds);

            await repository.AddAsync(notification);
            await repository.CommitAsync();

            if (notification.IsPushEnabled)
                await pushNotificationDispatcher.DispatchAsync(request.Title, request.Content, notification.Targets, notification.UserIds, cancellationToken);
        }
    }
}
