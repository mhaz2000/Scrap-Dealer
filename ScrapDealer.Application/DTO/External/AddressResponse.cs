namespace ScrapDealer.Application.DTO.External
{
    public record AddressResponse(
    string Status,
    string FormattedAddress,
    string RouteName,
    string RouteType,
    string Neighbourhood,
    string City,
    string State,
    string? Place,
    string MunicipalityZone,
    bool InTrafficZone,
    bool InOddEvenZone,
    string? Village,
    string County,
    string District
);
}
