using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Tickets.Handlers
{


    internal class AddMessageHandler(
        ITicketFactory ticketFactory,
        IUserRepository userRepository,
        ITicketMessageRepository ticketMessageRepository,
        ITicketRepository ticketRepository)
        : ICommandHandler<AddMessageCommand>
    {
        public async Task Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var ticket = await ticketRepository.GetAsync(t => t.Id == request.TicketId);
            if(ticket is null)
                throw new BusinessException("تیکت یافت نشد.");

            if(!ticket.Opened)
                throw new BusinessException("تیکت بسته شده است.");

            var user = await userRepository.GetAsync(t => t.Id == request.UserId);
            if (user is null)
                throw new BusinessException("کاربر یافت نشد.");

            var ticketMessage = ticketFactory.CreateMessage(user, request.Content, request.Attachments);

            ticket.AddMessage(ticketMessage);

            await ticketMessageRepository.AddAsync(ticketMessage);
        }
    }
}
