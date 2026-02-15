namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class RewardReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Amount { get; set; }
        public Guid UserId { get; set; }
        public UserReadModel User { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
    }
}
