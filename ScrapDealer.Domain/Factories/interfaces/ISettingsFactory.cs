using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Settings;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ISettingsFactory
    {
        Settings Create(Amount? commissionFixedAmount, CommissionRate? commissionRate);
        Settings Update(Amount? commissionFixedAmount, CommissionRate? commissionRate, Settings settings);
    }
}
