using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<InvoiceItemReadModel, InvoiceItemDto>()
                .ConstructUsing(t => new InvoiceItemDto()
                {
                    SaleType = t.SaleType,
                    Amount = t.Amount,
                    Weight = t.Weight,
                    Category = t.SubCategory == null ? string.Empty : t.SubCategory.Category.Name,
                    Subcategory = t.SubCategory == null ? string.Empty : t.SubCategory.Name
                })
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<InvoiceReadModel, InvoiceDto>()
                .ConstructUsing(s => new InvoiceDto
                {
                    Id = s.Id,
                    Amount = s.Amount,
                    InvoiceCode = s.Code,
                    SaleOrderCode = s.Contract.SaleOrder.Code,
                    BuyerName = s.Contract.Buyer.FirstName + " " + s.Contract.Buyer.LastName,
                    SellerName = s.Contract.SaleOrder.Seller.FirstName + " " + s.Contract.SaleOrder.Seller.LastName,
                    DateTime = s.DateTime,
                    Items = s.Items.Select(t => new InvoiceItemDto()
                    {
                        SaleType = t.SaleType,
                        Amount = t.Amount,
                        Weight = t.Weight,
                        Category = t.SubCategory == null ? string.Empty : t.SubCategory.Category.Name,
                        Subcategory = t.SubCategory == null ? string.Empty : t.SubCategory.Name
                    })
                })
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<InvoiceReadModel, InvoiceListDto>()
                .ConstructUsing(s => new InvoiceListDto
                {
                    Id = s.Id,
                    Amount = s.Amount,
                    InvoiceCode = s.Code,
                    SaleOrderCode = s.Contract.SaleOrder.Code,
                    BuyerName = s.Contract.Buyer.FirstName + " " + s.Contract.Buyer.LastName,
                    SellerName = s.Contract.SaleOrder.Seller.FirstName + " " + s.Contract.SaleOrder.Seller.LastName,
                    DateTime = s.DateTime
                });
        }
    }
}
