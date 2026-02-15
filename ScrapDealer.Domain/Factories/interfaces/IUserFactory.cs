using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Users;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IUserFactory
    {
        User Update(User user, Username username, Phone phone, string password, string? firstName = null, string? lastName = null);
        User Create(Username username, Phone phone, string password, string? firstName = null, string? lastName = null);
        User Create(Username username, Phone phone);
    }
}
