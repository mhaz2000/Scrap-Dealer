using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketReadModel, TicketDto>()
                .ForMember(dest => dest.CreatedBy, src => src.MapFrom(t => t.Messages.FirstOrDefault()!.Sender!.FirstName + " "
                + t.Messages.FirstOrDefault()!.Sender!.LastName));
        }
    }
}
