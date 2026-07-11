using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface IUserReadService
    {
        Task<bool> ExistsByUserNameAsync(string username, Guid? excludedId = null);
        Task<bool> ExistsByPhoneAsync(string username, Guid? excludedId = null);
        Task<Guid?> GetByPhoneAsync(string phone);
        Task<Guid?> ValidateUserCredentialByUsernameAsync(string username, string password);
        Task<bool> ValidateUserCredentialByUserIdAsync(Guid id, string password);
        Task<bool> CheckIfUserActiveAsync(Guid userId);
        Task<Guid?> GetIdByReferralCodeAsync(string code);
    }
}
