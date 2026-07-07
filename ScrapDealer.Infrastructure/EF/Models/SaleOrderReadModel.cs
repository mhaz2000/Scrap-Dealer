using ScrapDealer.Domain.Consts;
using System.Collections.ObjectModel;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class SaleOrderReadModel
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public required string Address { get; set; }
        public string? Telephone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool ModifiedByAdmin { get; set; }
        public required SellerReadModel Seller { get; set; }
        public Guid SellerId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsIndustrial { get; set; }
        public SaleOrderStatus Status { get; set; }
        public bool SaleAtBuyersLocation { get; set; }
        public string? RejectionReason { get; set; }
        public Collection<SaleOrderItemReadModel> Items { get; set; } = [];
    }
}
