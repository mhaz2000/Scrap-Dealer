using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SystemSettings;
public record UpdateSettingsCommand(decimal? BuyerCommissionFixedAmount, float? BuyerCommissionRate) : ICommand;
 
