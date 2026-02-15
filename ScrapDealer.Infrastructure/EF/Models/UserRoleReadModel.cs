namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class UserRoleReadModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public UserReadModel User { get; set; }
        public RoleReadModel Role { get; set; }
    }
}
