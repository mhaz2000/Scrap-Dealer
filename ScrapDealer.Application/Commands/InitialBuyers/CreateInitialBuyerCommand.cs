using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.InitialBuyers;

public record CreateInitialBuyerCommand(string FirstName, string LastName, string Phone, string? Area, bool HasVehicle) : ICommand;
