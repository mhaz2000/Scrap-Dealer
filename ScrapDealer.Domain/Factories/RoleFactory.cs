using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Roles;

namespace ScrapDealer.Domain.Factories
{

    public class RoleFactory : IRoleFactory
    {
        public Role Create(Guid id, RoleName roleName)
        {
            var roleNameValue = RoleName.Create(roleName);
            return new Role(id, roleNameValue);
        }
    }
}
