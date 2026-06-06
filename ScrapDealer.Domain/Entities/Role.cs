using ScrapDealer.Domain.ValueObjects.Roles;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Role : AggregateRoot<Guid>
    {
        public RoleName Name { get; private set; }

        private readonly List<UserRole> _userRoles = new List<UserRole>();
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

        private readonly List<RolePermission> _rolePermissions = new List<RolePermission>();
        public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

        private Role() { }
        public Role(Guid id, RoleName name) : base(id)
        {
            Name = name;
        }
    }

}
