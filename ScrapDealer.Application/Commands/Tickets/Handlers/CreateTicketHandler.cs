using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Tickets.Handlers
{

    internal class CreateTicketHandler(
        ITicketReadService ticketReadService,
        ITicketFactory ticketFactory,
        IUserRepository userRepository,
        ITicketRepository ticketRepository)
        : ICommandHandler<CreateTicketCommand, Guid>
    {
        public async Task<Guid> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(t => t.Id == request.UserId);
            if (user is null)
                throw new BusinessException("کاربر یافت نشد.");

            var lastTicketNumber = await ticketReadService.GetLastTickerNumberAsync();

            var ticket = ticketFactory.Create(request.Title, lastTicketNumber);
            var ticketMessage = ticketFactory.CreateMessage(user, request.Content, request.Attachments);

            ticket.AddMessage(ticketMessage);

            await ticketRepository.AddAsync(ticket);

            return ticket.Id;
        }
    }
}
