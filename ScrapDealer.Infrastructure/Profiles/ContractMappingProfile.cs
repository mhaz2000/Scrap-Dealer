using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class ContractMappingProfile : Profile
    {
        public ContractMappingProfile()
        {
            CreateMap<ContractReadModel, BuyerContractDto>()
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(b => $"{b.SaleOrder.Seller.FirstName} {b.SaleOrder.Seller.LastName}"))
                .ForMember(dest => dest.SellerScore, opt => opt.MapFrom(b => b.SaleOrder.Seller.Score));

            CreateMap<ContractReadModel, SellerContractDto>()
                .ForMember(dest => dest.IsFixedLocation, opt => opt.MapFrom(b => b.Buyer.IsFixedLocation))
                .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(b => $"{b.Buyer.FirstName} {b.Buyer.LastName}"))
                .ForMember(dest => dest.BuyerScore, opt => opt.MapFrom(b => b.Buyer.Score));

            CreateMap<ContractReadModel, SellerContractDetailDto>()
                .ForMember(dest => dest.IsFixedLocation, opt => opt.MapFrom(b => b.Buyer.IsFixedLocation))
                .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(b => $"{b.Buyer.FirstName} {b.Buyer.LastName}"))
                .ForMember(dest => dest.BuyerScore, opt => opt.MapFrom(b => b.Buyer.Score))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(b => b.Buyer.User.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(b => b.Buyer.IsFixedLocation ? b.Buyer.AddressDescription : ""))
                .ForMember(dest => dest.NumberPlate, opt => opt.MapFrom(b => b.Buyer.NumberPlate));

            CreateMap<ContractReadModel, BuyerContractDetailDto>()
                .ForMember(dest => dest.SellerLatitude, opt => opt.MapFrom(b => b.SaleOrder.Seller.Latitude))
                .ForMember(dest => dest.SellerLongitude, opt => opt.MapFrom(b => b.SaleOrder.Seller.Longitude))
                .ForMember(dest => dest.BuyerLatitude, opt => opt.MapFrom(b => b.Buyer.Latitude))
                .ForMember(dest => dest.BuyerLongitude, opt => opt.MapFrom(b => b.Buyer.Longitude))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(b => b.SaleOrder.Seller.AddressDescription))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(b => b.SaleOrder.Seller.User.Phone))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(b => $"{b.SaleOrder.Seller.FirstName} {b.SaleOrder.Seller.LastName}"))
                .ForMember(dest => dest.SellerScore, opt => opt.MapFrom(b => b.SaleOrder.Seller.Score));



        }
    }
}