using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.DTO;

public record SellerContractDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public required string BuyerName { get; set; }
    public float BuyerScore { get; set; }
    public bool IsFixedLocation { get; set; }
    public ContractStatus Status { get; private set; }

}
