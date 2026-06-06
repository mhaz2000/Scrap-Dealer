using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Sellers;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Sellers
{
    internal class GetSellerProfileHandler : IQueryHandler<GetSellerProfileQuery, SellerProfileDto>
    {
        private readonly DbSet<SellerReadModel> _sellers;
        private readonly DbSet<WalletReadModel> _wallets;
        private readonly IMapper _mapper;
        public GetSellerProfileHandler(ReadDbContext context, IMapper mapper)
        {
            _sellers = context.Sellers;
            _wallets = context.Wallets;
            _mapper = mapper;
        }
        public async Task<SellerProfileDto> Handle(GetSellerProfileQuery request, CancellationToken cancellationToken)
        {
            var seller = await _sellers.Include(c => c.User).FirstOrDefaultAsync(b => b.UserId == request.UserId);
            if(seller is null)
                throw new BusinessException("اطلاعات فروشنده یافت نشد.");

            var dto = _mapper.Map<SellerProfileDto>(seller);

            var wallet = await _wallets.Include(t => t.Buyer).Include(t => t.Seller)
                .FirstOrDefaultAsync(t => t.Seller!.UserId == request.UserId);

            return dto with { WalletNumber = wallet?.Number, WalletBalance = wallet?.Balance };
        }
    }
}
