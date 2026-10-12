using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Notifications.Handlers
{
    public class UpdateNotificationHandler(INotificationFactory factory, INotificationRepository repository) : ICommandHandler<UpdateNotificationCommand>
    {
        public async Task Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var hasTargets = request.targets is { Count: > 0 };
            var hasUsers = request.UserIds is { Count: > 0 };
            if (hasTargets == hasUsers)
                throw new BusinessException("اعلان باید دقیقا یک گروه هدف یا فهرست کاربران داشته باشد.");

            var notification = await repository.GetAsync(c => c.Id == request.Id);
            if (notification is null)
                throw new BusinessException("اعلان یافت نشد.");

            notification = factory.Update(request.Title, request.Content, request.targets, notification, request.IsPushEnabled, request.UserIds?.Distinct().ToList());
            notification.SeenBy = [];
            await repository.UpdateAsync(notification);
        }
    }
}
