using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Supports;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Supports
{
    internal class GetSupportHandler(ReadDbContext _context, IMapper _mapper) : IQueryHandler<GetSupportQuery, SupportDto>
    {
        private readonly DbSet<UserReadModel> _users = _context.Users;
        public async Task<SupportDto> Handle(GetSupportQuery request, CancellationToken cancellationToken)
        {
            var support = await _users.FirstOrDefaultAsync(t=> t.Id ==  request.Id);
            if (support is null)
                throw new BusinessException("اطلاعات پشتیبان یافت نشد");

            return _mapper.Map<SupportDto>(support);
        }
    }
}
