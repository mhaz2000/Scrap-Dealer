using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IInvoiceFactory
    {
        Invoice Create(Contract contract, Amount amount, Code code);
        InvoiceItem CreateItem(SubCategory? subCategory, SaleType saleType, Amount amount, double? weight);
    }
}
