namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class SettingsReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public decimal? BuyerCommissionFixedAmount { get; private set; }
        public float? BuyerCommissionRate { get; private set; }
    }
}
