namespace ScrapDealer.Application.DTO;

public record InitialBuyerDto(Guid Id, string FirstName, string LastName, string Phone, string? Area, bool HasVehicle, bool OwnsShop);
