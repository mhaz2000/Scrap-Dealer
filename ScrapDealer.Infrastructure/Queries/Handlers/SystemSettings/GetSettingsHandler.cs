using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.SystemSettings;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.SystemSettings
{
    internal class GetSettingsHandler(ReadDbContext _context, IMapper _mapper) : IQueryHandler<GetSettingsQuery, SettingsDto>
    {
        private readonly DbSet<SettingsReadModel> _settings = _context.Settings;

        public async Task<SettingsDto> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _settings.FirstOrDefaultAsync();

            return _mapper.Map<SettingsDto>(settings);
        }
    }
}
