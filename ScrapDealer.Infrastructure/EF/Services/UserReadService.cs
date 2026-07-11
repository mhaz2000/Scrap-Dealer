using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{

    internal sealed class UserReadService : IUserReadService
    {
        private readonly DbSet<UserReadModel> _users;
        private readonly IPasswordHasher<object> _passwordHasher;

        public UserReadService(ReadDbContext context, IPasswordHasher<object> passwordHasher)
        {
            _users = context.Users;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> CheckIfUserActiveAsync(Guid userId) 
            => (await _users.FirstOrDefaultAsync(t=> t.Id == userId))!.IsActive;

        public Task<bool> ExistsByPhoneAsync(string phone, Guid? excludedId)
            => _users.AnyAsync(u => u.Phone == phone && (excludedId != null ? u.Id != excludedId : true));

        public Task<bool> ExistsByUserNameAsync(string username, Guid? excludedId)
            => _users.AnyAsync(u => u.Username == username.ToLower() && (excludedId != null ? u.Id != excludedId : true));

        public async Task<Guid?> ValidateUserCredentialByUsernameAsync(string username, string password)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Username == username);

            return user is not null &&
                _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success
                ? user?.Id : null;

        }

        public async Task<bool> ValidateUserCredentialByUserIdAsync(Guid id, string password)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Id == id);

            return user is not null &&
                _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;

        }

        public async Task<Guid?> GetByPhoneAsync(string phone)
            => (await _users.FirstOrDefaultAsync(c => c.Phone == phone).ConfigureAwait(false))?.Id;

        public Task<Guid?> GetIdByReferralCodeAsync(string code)
            => _users.Where(u => u.ReferralCode == code).Select(u => (Guid?)u.Id).FirstOrDefaultAsync();
    }
}
