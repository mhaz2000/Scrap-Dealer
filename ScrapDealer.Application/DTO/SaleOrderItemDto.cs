using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.DTO
{
    public record SaleOrderItemDto
    {
        public string? SystemDescription { get; set; }
        public string? SellerDescription { get; set; }
        public ICollection<Guid> Images { get; set; }
        public SubCategoryDto? SubCategory { get; set; }
        public SaleType? SaleType { get; set; }
        public Guid Id { get; set; }
    }
}
