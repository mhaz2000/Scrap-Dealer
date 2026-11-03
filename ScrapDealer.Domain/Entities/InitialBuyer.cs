using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities;

public class InitialBuyer : AggregateRoot<Guid>
{
    public PersonName PersonName { get; private set; }
    public Phone Phone { get; private set; }
    public string? Area { get; private set; }
    public bool HasVehicle { get; private set; }

    private InitialBuyer() { }

    public InitialBuyer(PersonName personName, Phone phone, string? area, bool HasVehicle)
    {
        Id = Guid.NewGuid();
        PersonName = personName;
        Phone = phone;
        Area = area;
        HasVehicle = HasVehicle;
    }
}
