using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.Factories
{
    public class InvoiceFactory : IInvoiceFactory
    {
        public Invoice Create(Contract contract, Amount amount, Code code)
        {
            var amountValue = Amount.Create(amount);

            return new Invoice(contract, amountValue, code);
        }

        public InvoiceItem CreateItem(SubCategory? subCategory, SaleType saleType, Amount amount, Weight? weight)
        {
            var amountValue = Amount.Create(amount);
            var weightValue = weight is null ? null : Weight.Create(weight);

            return new InvoiceItem(amountValue, subCategory, saleType, weightValue);
        }
    }
}
