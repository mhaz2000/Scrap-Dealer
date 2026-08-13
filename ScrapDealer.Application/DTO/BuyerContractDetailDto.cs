using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.DTO;

public record BuyerContractDetailDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public required string SellerName { get; set; }
    public float SellerScore { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public ContractStatus Status { get; private set; }

}