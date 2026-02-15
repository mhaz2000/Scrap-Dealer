using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Notifications.Handlers
{
    public class SeenNotificationHandler(INotificationRepository repository) : ICommandHandler<SeenNotificationCommand>
    {
        public async Task Handle(SeenNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await repository.GetAsync(c => c.Id == request.Id);
            if (notification is null)
                throw new BusinessException("اعلان یافت نشد.");

            notification.SeenBy.Add(request.UserId);
            await repository.UpdateAsync(notification);
        }
    }
}
