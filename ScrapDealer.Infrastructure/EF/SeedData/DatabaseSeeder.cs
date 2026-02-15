using Microsoft.EntityFrameworkCore;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Shared.SystemPermissions;

namespace ScrapDealer.Infrastructure.EF.SeedData
{
    internal class DatabaseSeeder
    {
        private readonly WriteDbContext _context;
        private readonly IUserFactory _userFactory;

        public DatabaseSeeder(WriteDbContext context, IUserFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }

        public async Task SeedAsync()
        {
            Role? supportRole;
            if (!_context.Users.Any() && !_context.Roles.Any())
            {
                var adminRole = new Role(Guid.NewGuid(), "Admin");
                supportRole = new Role(Guid.NewGuid(), "Support");
                var sellerRole = new Role(Guid.NewGuid(), "Seller");
                var buyerRole = new Role(Guid.NewGuid(), "Buyer");

                var admin = _userFactory.Create("admin", "09100000000", "admin123");
                admin.AddRole(adminRole);

                _context.Roles.AddRange(adminRole, sellerRole, buyerRole, supportRole);
                _context.Users.Add(admin);

                await _context.SaveChangesAsync();
            }

            supportRole = await _context.Roles.FirstOrDefaultAsync(c => c.Name == "Support");
            var rolePermissions = _context.RolePermissions.Select(s => s.PermissionName.Value);

            var permissions = Permissions.GetAllPermissions();

            foreach (var permission in permissions.Except(rolePermissions))
            {
                var rolePermission = new RolePermission(permission, supportRole!);
                _context.RolePermissions.Add(rolePermission);
            }

            await _context.SaveChangesAsync();
        }
    }
}
