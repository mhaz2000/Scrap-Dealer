using ScrapDealer.Application.DTO.External;
using ScrapDealer.Application.Services;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Addresses;

public class LocationHandler(ILocationService locationService) 
    : ICommandHandler<LocationCommand, AddressMapIRResponse>
{
    public async Task<AddressMapIRResponse> Handle(LocationCommand request, CancellationToken cancellationToken)
        => await locationService.GetLocationAsync(request);
}