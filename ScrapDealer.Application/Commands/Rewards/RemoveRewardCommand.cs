using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Rewards;

public record RemoveRewardCommand(Guid Id) : ICommand;

