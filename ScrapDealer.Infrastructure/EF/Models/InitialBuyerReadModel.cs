namespace ScrapDealer.Infrastructure.EF.Models;

internal class InitialBuyerReadModel
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Phone { get; set; }
    public string? Area { get; set; }
    public bool HasVehicle { get; set; }
    public bool OwnsShop { get; set; }
}
