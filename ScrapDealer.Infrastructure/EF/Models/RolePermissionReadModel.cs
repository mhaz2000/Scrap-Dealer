
namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class RolePermissionReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public required string PermissionName { get; set; }
        public Guid RoleId { get; set; }
        public RoleReadModel Role { get; set; }

    }
}
