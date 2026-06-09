using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.SystemPermissions;

namespace ScrapDealer.Application.Commands.SystemPermissions.Handlers;

internal class UpdatePermissionHandler(IRolePermissionRepository _repository, IRoleRepository _roleRepository, IRolePermissionFactory _factory)
    : ICommandHandler<UpdatePermissionCommand>
{
    public async Task Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var supportRole = await _roleRepository.GetAsync(t => t.Name == "Support"); //As support is the only role that gets permissions.
        _repository.ClearAll();

        if (request.Permissions.Any(t => !Permissions.GetAllPermissions().Contains(t)))
            throw new BusinessException("نام دسترسی نامعتبر است.");

        foreach (var permission in request.Permissions)
        {
            var permissionToAdd = _factory.Create(permission, supportRole!);
            await _repository.AddAsync(permissionToAdd);
            await _repository.CommitAsync();
        }
    }
}
