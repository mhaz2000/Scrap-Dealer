using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Contracts;
public record CancelContractCommand(Guid UserId) : ICommand;
