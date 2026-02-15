using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Rewards;
public record AddRewardCommand(decimal Amount, string? Description, Guid UserId) : ICommand;

