using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Supports.Handlers
{
    internal class AddSupportHandler : ICommandHandler<AddSupportCommand>
    {
        private readonly IUserReadService _readService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleReadService _roleReadService;
        private readonly IUserFactory _factory;

        public AddSupportHandler(IUserRepository userRepository, IUserReadService readService, 
            IRoleReadService roleReadService, IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _readService = readService;
            _roleReadService = roleReadService;
            _factory = userFactory;
        }

        public async Task Handle(AddSupportCommand request, CancellationToken cancellationToken)
        {

            if (await _readService.ExistsByPhoneAsync(request.PhoneNumber))
                throw new BusinessException("کاربری قبلا با این موبایل ثبت شده است.");

            if (await _readService.ExistsByUserNameAsync(request.Username))
                throw new BusinessException("کاربری قبلا با این نام ثبت شده است.");

            var roleId = await _roleReadService.GetRoleIdByNameAsync("Support");

            var user = _factory.Create(request.Username, request.PhoneNumber, request.Password, request.FirstName, request.LastName);

            user.AddRole(new Role(roleId, "Support"));

            await _userRepository.AddAsync(user);
        }
    }
}
