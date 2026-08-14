using AutoMapper;
using DNTPersianUtils.Core;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class SaleOrderRequestProfile : Profile
    {
        public SaleOrderRequestProfile()
        {
            CreateMap<SaleOrderRequestReadModel, SaleOrderRequestDto>()
                .ConstructUsing(t => new SaleOrderRequestDto()
                {
                    Id = t.Id,
                    SellerName = t.SaleOrder.Seller.FirstName + " " + t.SaleOrder.Seller.LastName,
                    IsIndustrial = t.SaleOrder.IsIndustrial,
                    SaleOrderCode = t.SaleOrder.Code,
                    SellerScore = t.SaleOrder.Seller.Score,
                    SaleOrderId = t.SaleOrder.Id,
                    Latitude = t.SaleOrder.Latitude,
                    Longitude = t.SaleOrder.Longitude,
                    Items = t.SaleOrder.Items.Select(s => new SaleOrderItemDto()
                    {
                        Id = s.Id,
                        Images = s.Images,
                        SaleType = s.SaleType,
                        SellerDescription = s.SellerDescription,
                        SystemDescription = s.SystemDescription,
                        SubCategory = s.SubCategory == null ? null :
                                        new SubCategoryDto(s.SubCategory.Id, s.SubCategory.Name, s.SubCategory.MinPrice, s.SubCategory.MaxPrice, s.SubCategory.LastUpdate.ToPersianDateTimeString("yyyy/MM/dd HH:mm", false), s.SubCategory.CategoryId, s.SubCategory.Images)
                    })
                }).ForAllMembers(t => t.Ignore());
        }
    }
}
