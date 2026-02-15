using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Notifications.Handlers
{
    public class DeleteNotificationHandler(INotificationRepository repository) : ICommandHandler<DeleteNotificationCommand>
    {
        public async Task Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await repository.GetAsync(c => c.Id == request.Id);
            if (notification is null)
                throw new BusinessException("اعلان یافت نشد.");

            await repository.DeleteAsync(notification.Id);
        }
    }
}
