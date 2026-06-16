using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class ScoreHistoryReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public ScoreFor ScoreFor { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid ContractId { get; set; }
        public BuyerReadModel Buyer { get; set; }
        public SellerReadModel Seller { get; set; }
        public ContractReadModel Contract { get; set; }
        public float Score { get; set; }
        public string? Comment { get; set; }
    }
}
