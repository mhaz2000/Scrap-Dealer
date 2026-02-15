using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Roles;

namespace ScrapDealer.Domain.Factories
{
    public class RolePermissionFactory : IRolePermissionFactory
    {
        public RolePermission Create(PermissionName name, Role role)
        {
            var permissionNameValue = PermissionName.Create(name);

            return new RolePermission(permissionNameValue, role);
        }
    }
}
