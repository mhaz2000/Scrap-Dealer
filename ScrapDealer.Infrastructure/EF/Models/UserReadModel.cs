using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class UserReadModel
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Phone { get; set; }
        public required string PasswordHash { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<UserRoleReadModel> UserRoles { get; set; }

        public string FullName => FirstName + " " + LastName;
    }
}
