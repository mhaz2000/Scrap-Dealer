using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class ScoreHistory : AggregateRoot<Guid>
    {
        public Score Score { get; set; }
        public Guid BuyerId { get; set; }
        public Buyer Buyer { get; set; }
        public Guid SellerId { get; set; }
        public Seller Seller { get; set; }
        public Contract Contract { get; set; }
        public Guid ContractId { get; set; }
        public ScoreFor ScoreFor { get; set; }
        public string? Comment { get; set; }

        private ScoreHistory() { }

        public ScoreHistory(Score score, Buyer buyer, Seller seller, Contract contract, ScoreFor scoreFor, string? comment)
        {
            Score = score;
            Buyer = buyer;
            Seller = seller;
            Contract = contract;
            SellerId = seller.Id;
            BuyerId = buyer.Id;
            ContractId = contract.Id;
            Comment = comment;
        }
    }
}
