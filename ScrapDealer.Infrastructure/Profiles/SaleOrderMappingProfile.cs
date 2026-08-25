using AutoMapper;
using DNTPersianUtils.Core;
using DocumentFormat.OpenXml.Office2010.Excel;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    internal class SaleOrderMappingProfile : Profile
    {
        public SaleOrderMappingProfile()
        {
            CreateMap<SaleOrderReadModel, SaleOrderDto>()
                .ConstructUsing(c => new SaleOrderDto()
                {
                    Id = c.Id,
                    Address = c.Address,
                    SellerName = c.Seller.FirstName + " " + c.Seller.LastName,
                    IsIndustrial = c.IsIndustrial,
                    Status = c.Status,
                    ModifiedByAdmin = c.ModifiedByAdmin,
                    RejectionReason = c.RejectionReason,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    SaleAtBuyersLocation = c.SaleAtBuyersLocation,
                    Telephone = c.Telephone,
                    Code = c.Code,
                    Items = c.Items.Select(s => new SaleOrderItemDto()
                    {
                        Id = s.Id,
                        Images = s.Images,
                        SaleType = s.SaleType,
                        SellerDescription = s.SellerDescription,
                        SystemDescription = s.SystemDescription,
                        SubCategory = s.SubCategory == null ? null : 
                            new SubCategoryDto(s.SubCategory.Id, s.SubCategory.Name, s.SubCategory.MinPrice, s.SubCategory.MaxPrice, s.SubCategory.LastUpdate.ToPersianDateTimeString("yyyy/MM/dd HH:mm", false), s.SubCategory.CategoryId, s.SubCategory.Images),
                        ModifiedByAdmin = s.ModifiedByAdmin
                    })
                }).ForAllMembers(opt => opt.Ignore());
        }
    }
}
