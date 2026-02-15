using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Notifications.Handlers
{
    public class AddNotificationHandler(INotificationFactory factory, INotificationRepository repository) : ICommandHandler<AddNotificationCommand>
    {
        public async Task Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = factory.Create(request.Title, request.Content);

            await repository.AddAsync(notification);
        }
    }
}
