using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Tickets;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Tickets
{
    internal class GetTicketHandler(ReadDbContext dbContext) : IQueryHandler<GetTicketQuery, TicketDetailDto>
    {
        private readonly DbSet<TicketReadModel> _tickets = dbContext.Tickets;
        private readonly DbSet<SellerReadModel> _sellers = dbContext.Sellers;
        private readonly DbSet<BuyerReadModel> _buyers = dbContext.Buyers;
        public async Task<TicketDetailDto> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _tickets.Include(t => t.Messages).ThenInclude(t => t.Sender).FirstOrDefaultAsync(t => t.Id == request.Id);
            if (ticket is null)
                throw new BusinessException("تیکت یافت نشد.");

            var usersId = _sellers.Select(s => s.UserId).ToList().Union(_buyers.Select(b => b.UserId).ToList());

            return new TicketDetailDto()
            {
                Id = request.Id,
                Opened = ticket.Opened,
                Title = ticket.Title,
                TicketNumber = ticket.Number,
                Messages = ticket.Messages.Select(message => new MessageDto()
                {
                    Id = message.Id,
                    Content = message.Content,
                    Attachments = message.Attachments,
                    Sender = message.Sender.FirstName + " " + message.Sender.LastName,
                    SenderId = message.Sender.Id,
                    IsUserMessage = usersId.Contains(message.SenderId)
                })
            };
        }
    }
}
