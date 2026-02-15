using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Application.Queries.Users;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Users
{

    internal sealed class GetUserProfileHandler : IQueryHandler<GetUserProfileQuery, UserDto>
    {
        private readonly DbSet<UserReadModel> _users;
        private readonly IMapper _mapper;

        public GetUserProfileHandler(ReadDbContext context, IMapper mapper)
        {
            _users = context.Users;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Id == query.userId);
            return _mapper.Map<UserDto>(user);
        }
    }


}
