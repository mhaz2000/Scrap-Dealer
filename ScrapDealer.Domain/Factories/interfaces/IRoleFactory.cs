using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Roles;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IRoleFactory
    {
        Role Create(Guid id, RoleName roleName);
    }
}
