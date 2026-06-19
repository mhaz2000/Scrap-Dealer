using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.DTO
{
    public record SaleOrderRequestDto
    {
        public Guid Id { get; set; }
        public bool IsIndustrial { get; set; }
        public required string SellerName { get; set; }
        public float SellerScore { get; set; }
        public IEnumerable<SaleOrderItemDto> Items { get; set; } = [];
    }
}
