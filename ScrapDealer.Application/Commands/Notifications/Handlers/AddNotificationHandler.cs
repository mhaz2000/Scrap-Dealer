using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Notifications.Handlers
{
    public class AddNotificationHandler(INotificationFactory factory, INotificationRepository repository) : ICommandHandler<AddNotificationCommand>
    {
        public async Task Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.UserRole == "Support" && request.targets.Any(t => t == NotificationTarget.Support))
                throw new BusinessException("پشتیبان نمی‌تواند برای پشتیبان‌ها اعلان ایجاد کند.");

            var notification = factory.Create(request.Title, request.Content, request.targets);

            await repository.AddAsync(notification);
            await repository.CommitAsync();
        }
    }
}
