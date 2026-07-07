using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.DTO
{
    public record SaleOrderDto
    {
        public Guid Id { get; set; }
        public required string Address { get; set; }
        public string? Telephone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool SaleAtBuyersLocation { get; set; }
        public bool IsIndustrial { get; set; }
        public required string SellerName { get; set; }
        public string? RejectionReason { get; set; }
        public bool ModifiedByAdmin { get; set; }
        public SaleOrderStatus Status { get; set; }
        public IEnumerable<SaleOrderItemDto> Items { get; set; } = [];
        public bool? HasFinishedOrOngoingContract { get; set; }
        public Guid? ContractId { get; set; }
        public Guid? RequestId { get; set; }
        public string? SendRequestTo { get; set; }
        public int Code { get; set; }
    }
}
