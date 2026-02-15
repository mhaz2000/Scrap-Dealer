using Microsoft.AspNetCore.Identity;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Users.Handlers
{
    internal class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUserReadService _readService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ChangePasswordHandler(IUserReadService readService, IPasswordHasher<User> passwordHasher, IUserRepository userRepository)
        {
            _readService = readService;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }
        public async Task Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var (UserId, Password, OldPassword) = command;
            if (!await _readService.ValidateUserCredentialByUserIdAsync(UserId, OldPassword))
                throw new BusinessException("رمز عبور اشتباه می‌باشد.");

            var user = await _userRepository.GetAsync(u => u.Id == UserId);
            user!.SetPassword(PasswordHash.Create(Password, _passwordHasher));

            await _userRepository.UpdateAsync(user);
        }
    }
}
