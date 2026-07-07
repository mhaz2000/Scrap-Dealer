using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.SaleOrders;
using ScrapDealer.Shared.Abstractions.Domain;
using System.Collections.ObjectModel;

namespace ScrapDealer.Domain.Entities
{

    public class SaleOrder : AggregateRoot<Guid>
    {
        public Seller Seller { get; private set; }
        public SaleOrderAddress Address { get; set; }
        public Telephone? Telephone { get; set; }
        public Location? Location { get; set; }
        public Guid SellerId { get; private set; }
        public bool IsIndustrial { get; private set; }
        public bool SaleAtBuyersLocation { get; private set; }
        public SaleOrderStatus Status { get; set; }
        public bool ModifiedByAdmin { get; set; } = false;
        public string? RejectionReason { get; set; }
        public Code Code { get; private set; }

        private readonly List<SaleOrderItem> _items = new List<SaleOrderItem>();
        public IReadOnlyCollection<SaleOrderItem> Items => _items.AsReadOnly();

        public SaleOrder()
        {

        }

        public SaleOrder(bool isIndustrial, Seller seller, SaleOrderAddress address, Location? location, Telephone? telephone, bool saleAtBuyersLocation, Code code)
        {
            Seller = seller;
            Address = address;
            SellerId = seller.Id;
            IsIndustrial = isIndustrial;
            Location = location;
            Status = SaleOrderStatus.CreatedOrUpdated;
            Telephone = telephone;
            SaleAtBuyersLocation = saleAtBuyersLocation;
            Code = code;
        }

        public void AddItem(SaleOrderItem item) 
            => _items.Add(item);

        internal void Update(SaleOrderAddress address, Location? location, Telephone? telephone, bool saleAtBuyersLocation)
        {
            ModifiedByAdmin = false;
            RejectionReason = null;
            Status = SaleOrderStatus.CreatedOrUpdated;

            Address = address;
            Location = location;
            Telephone = telephone;
            SaleAtBuyersLocation = saleAtBuyersLocation;
        }

        internal void SetAsUpdated() => ModifiedByAdmin = true;

        public void UpdateStatus(SaleOrderStatus status, string? rejectionReason)
        {
            Status = status;
            RejectionReason = rejectionReason;
        }
    }
}
