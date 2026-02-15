using ScrapDealer.Domain.ValueObjects.Roles;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class RolePermission : AggregateRoot<Guid>
    {
        public Guid RoleId { get; private set; }
        public PermissionName PermissionName { get; private set; }

        public Role Role { get; set; } = default!;

        public RolePermission()
        {
            
        }
        public RolePermission(PermissionName name, Role role)
        {
            PermissionName = name;
            Role = role;
            RoleId = Role.Id;
        }
    }

}
