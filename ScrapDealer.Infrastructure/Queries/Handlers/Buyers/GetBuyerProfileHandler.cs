using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Buyers;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Buyers
{
    internal class GetBuyerProfileHandler : IQueryHandler<GetBuyerProfileQuery, BuyerProfileDto>
    {
        private readonly DbSet<BuyerReadModel> _buyers;
        private readonly DbSet<WalletReadModel> _wallets;
        private readonly IMapper _mapper;
        public GetBuyerProfileHandler(ReadDbContext context, IMapper mapper)
        {
            _buyers = context.Buyers;
            _wallets = context.Wallets;
            _mapper = mapper;
        }
        public async Task<BuyerProfileDto> Handle(GetBuyerProfileQuery request, CancellationToken cancellationToken)
        {
            var buyer = await _buyers.Include(c => c.User).FirstOrDefaultAsync(b => b.UserId == request.UserId);
            if (buyer is null)
                throw new BusinessException("اطلاعات خریدار یافت نشد.");

            var dto = _mapper.Map<BuyerProfileDto>(buyer);

            var wallet = await _wallets.Include(t => t.Buyer).Include(t => t.Seller)
                .FirstOrDefaultAsync(t => t.Buyer!.UserId == request.UserId);

            return dto with { WalletNumber = wallet?.Number, WalletBalance = wallet?.Balance };
        }
    }
}
