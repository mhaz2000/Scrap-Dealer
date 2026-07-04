using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Application.Queries.Users;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Users;

internal sealed class GetUserStateHandler : IQueryHandler<GetUserStateQuery, UserStateDto>
{
    private readonly DbSet<UserReadModel> _users;
    private readonly DbSet<SellerReadModel> _sellers;
    private readonly DbSet<BuyerReadModel> _buyers;

    public GetUserStateHandler(ReadDbContext context)
    {
        _users = context.Users;
        _buyers = context.Buyers;
        _sellers = context.Sellers;
    }

    public async Task<UserStateDto> Handle(GetUserStateQuery query, CancellationToken cancellationToken)
    {
        var user = await _users.FirstOrDefaultAsync(u => u.Id == query.userId);
        if (user is null)
            throw new BusinessException("کاربر یافت نشد.");

        var buyer = await _buyers.FirstOrDefaultAsync(t => t.UserId == query.userId);
        var seller = await _sellers.FirstOrDefaultAsync(t => t.UserId == query.userId);

        return new UserStateDto
        {
            BuyerId = buyer?.Id,
            SellerId = seller?.Id,
            Verified = (buyer?.Verified ?? false) || (seller?.Verified ?? false)
        };
    }
}
