using ScrapDealer.Application.DTO.External;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Addresses;

public record LocationCommand(double Latitude, double Longitude) : ICommand<AddressMapIRResponse>;
