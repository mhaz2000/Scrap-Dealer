using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.SystemSettings;
public record GetSettingsQuery() : IQuery<SettingsDto>;
