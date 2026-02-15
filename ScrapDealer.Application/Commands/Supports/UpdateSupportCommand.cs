using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Supports;

public record UpdateSupportCommand(string Username, string Password, string FirstName, string LastName, string PhoneNumber, Guid Id) : ICommand;
public record DeleteSupportCommand(Guid Id) : ICommand;
