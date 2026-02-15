using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SystemPermissions;
public record UpdatePermissionCommand(IEnumerable<string> Permissions) : ICommand;
