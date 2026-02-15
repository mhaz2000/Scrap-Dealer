using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Tickets.Handlers
{
    internal class CloseTicketHandler(ITicketRepository repository) : ICommandHandler<CloseTicketCommand>
    {
        public async Task Handle(CloseTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await repository.GetAsync(t => t.Id == request.Id);
            if (ticket is null)
                throw new BusinessException("تیکت یافت نشد.");

            if (!ticket.Opened)
                throw new BusinessException("تیکت بسته شده است.");

            ticket.Opened = false;

            await repository.UpdateAsync(ticket);
        }
    }
}
