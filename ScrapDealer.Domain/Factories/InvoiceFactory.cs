using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.Factories
{
    public class InvoiceFactory : IInvoiceFactory
    {
        public Invoice Create(Contract contract, Amount amount, Code code)
        {
            var amountValue = Amount.Create(amount);

            return new Invoice(contract, amountValue, code);
        }

        public InvoiceItem CreateItem(SubCategory? subCategory, SaleType saleType, Amount amount, double? weight)
        {
            var amountValue = Amount.Create(amount);

            if (saleType == SaleType.Kilogram && (!weight.HasValue || weight.Value <= 0))
                throw new BusinessException("وزن برای کالاهای وزنی الزامی است.");

            Weight? weightValue = null;
            if (weight.HasValue)
                weightValue = Weight.Create(weight.Value);

            return new InvoiceItem(amountValue, subCategory, saleType, weightValue);
        }
    }
}
