using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Contracts;

public record VoteForContractCommand(Guid ContractId, float Score, string? Comment, Guid? UserId) : ICommand;
