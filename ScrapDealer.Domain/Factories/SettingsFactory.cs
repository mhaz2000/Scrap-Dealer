using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Settings;

namespace ScrapDealer.Domain.Factories
{
    public class SettingsFactory : ISettingsFactory
    {
        public Settings Create(Amount? commissionFixedAmount, CommissionRate? commissionRate)
        {
            var amountValue = commissionFixedAmount is null ? null : Amount.Create(commissionFixedAmount);
            var commissionRateValue = commissionRate is null ? null : CommissionRate.Create(commissionRate);

            return new Settings(commissionRateValue, amountValue);
        }

        public Settings Update(Amount? commissionFixedAmount, CommissionRate? commissionRate, Settings settings)
        {
            var amountValue = commissionFixedAmount is null ? null : Amount.Create(commissionFixedAmount);
            var commissionRateValue = commissionRate is null ? null : CommissionRate.Create(commissionRate);

            settings.Update(commissionRateValue, amountValue);
            return settings;
        }
    }
}
