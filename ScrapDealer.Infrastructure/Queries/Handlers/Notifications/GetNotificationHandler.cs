using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Notifications;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Notifications;
internal class GetNotificationHandler : IQueryHandler<GetNotificationQuery, NotificationDto>
{
    private readonly DbSet<NotificationReadModel> _notifications;
    private readonly IMapper _mapper;

    public GetNotificationHandler(ReadDbContext context, IMapper mapper)
    {
        _notifications = context.Notifications;
        _mapper = mapper;
    }
    public async Task<NotificationDto> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
    {
        var notification = await _notifications.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (notification is null)
            throw new BusinessException("اعلان یافت نشد.");

        if (request.UserRole != "Admin")
        {
            var allowed = request.UserRole switch
            {
                "Support" => notification.Targets.Any(t => t == NotificationTarget.Seller || t == NotificationTarget.Buyer),
                "Seller" => notification.Targets.Contains(NotificationTarget.Seller),
                "Buyer" => notification.Targets.Contains(NotificationTarget.Buyer),
                _ => false
            };

            if (!allowed)
                throw new BusinessException("شما مجوز مشاهده این اعلان را ندارید.");
        }

        var notificationDto = _mapper.Map<NotificationDto>(notification);

        notificationDto.Seen = notification.SeenBy.Any(id => id == request.UserId);

        return notificationDto;
    }
}
