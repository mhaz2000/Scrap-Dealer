using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.Factories
{
    public class InvoiceFactory : IInvoiceFactory
    {
        public Invoice Create(Contract contract, Amount amount)
        {
            var amountValue = Amount.Create(amount);

            return new Invoice(contract, amountValue);
        }

        public InvoiceItem CreateItem(SubCategory? subCategory, SaleType saleType, Amount amount)
        {
            var amountValue = Amount.Create(amount);

            return new InvoiceItem(amountValue, subCategory, saleType);
        }
    }
}
