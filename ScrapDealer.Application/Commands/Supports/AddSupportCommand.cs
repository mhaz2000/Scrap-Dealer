using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Supports;
public record AddSupportCommand(string Username, string Password, string FirstName, string LastName, string PhoneNumber) : ICommand;
