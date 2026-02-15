using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Sellers;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Sellers
{
    internal class GetSellerProfileHandler : IQueryHandler<GetSellerProfileQuery, SellerProfileDto>
    {
        private readonly DbSet<SellerReadModel> _sellers;
        private readonly IMapper _mapper;
        public GetSellerProfileHandler(ReadDbContext context, IMapper mapper)
        {
            _sellers = context.Sellers;
            _mapper = mapper;
        }
        public async Task<SellerProfileDto> Handle(GetSellerProfileQuery request, CancellationToken cancellationToken)
        {
            var seller = await _sellers.Include(c => c.User).FirstOrDefaultAsync(b => b.UserId == request.UserId);
            if(seller is null)
                throw new BusinessException("اطلاعات فروشنده یافت نشد.");

            return _mapper.Map<SellerProfileDto>(seller);
        }
    }
}
