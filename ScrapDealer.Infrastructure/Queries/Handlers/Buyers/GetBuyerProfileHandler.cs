using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Buyers;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Buyers
{
    internal class GetBuyerProfileHandler : IQueryHandler<GetBuyerProfileQuery, BuyerProfileDto>
    {
        private readonly DbSet<BuyerReadModel> _buyers;
        private readonly IMapper _mapper;
        public GetBuyerProfileHandler(ReadDbContext context, IMapper mapper)
        {
            _buyers = context.Buyers;
            _mapper = mapper;
        }
        public async Task<BuyerProfileDto> Handle(GetBuyerProfileQuery request, CancellationToken cancellationToken)
        {
            var buyer = await _buyers.Include(c => c.User).FirstOrDefaultAsync(b => b.UserId == request.UserId);
            if (buyer is null)
                throw new BusinessException("اطلاعات خریدار یافت نشد.");

            return _mapper.Map<BuyerProfileDto>(buyer);
        }
    }
}
