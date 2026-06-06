using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Application.Queries.Users;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Users;

internal sealed class GetUserProfileHandler : IQueryHandler<GetUserProfileQuery, UserDto>
{
    private readonly DbSet<UserReadModel> _users;
    private readonly DbSet<WalletReadModel> _wallets;
    private readonly IMapper _mapper;

    public GetUserProfileHandler(ReadDbContext context, IMapper mapper)
    {
        _users = context.Users;
        _wallets = context.Wallets;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
    {
        var user = await _users.FirstOrDefaultAsync(u => u.Id == query.userId);
        var dto = _mapper.Map<UserDto>(user);

        var wallet = await _wallets.Include(t => t.Buyer).Include(t => t.Seller)
            .FirstOrDefaultAsync(t => t.Seller.UserId == query.userId || t.Buyer.UserId == query.userId);

        return dto with { WalletNumber = wallet?.Number, WalletBalance = wallet?.Balance };
    }
}
