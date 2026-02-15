
namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class RoleReadModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<UserRoleReadModel> UserRoles { get; set; }
        public ICollection<RolePermissionReadModel> RolePermissions { get; set; }

        public bool IsDeleted { get; set; }
    }
}
