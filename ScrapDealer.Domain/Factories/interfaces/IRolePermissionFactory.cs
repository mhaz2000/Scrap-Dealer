using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Roles;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IRolePermissionFactory
    {
        RolePermission Create(PermissionName name, Role role);
    }
}
