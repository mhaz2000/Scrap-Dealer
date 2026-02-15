using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Supports.Handlers
{
    internal class UpdateSupportHandler : ICommandHandler<UpdateSupportCommand>
    {
        private readonly IUserFactory _factory;
        private readonly IUserReadService _readService;
        private readonly IUserRepository _userRepository;

        public UpdateSupportHandler(IUserFactory factory, IUserReadService readService, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _factory = factory;
            _readService = readService;
        }
        public async Task Handle(UpdateSupportCommand request, CancellationToken cancellationToken)
        {
            if (await _readService.ExistsByPhoneAsync(request.PhoneNumber, request.Id))
                throw new BusinessException("کاربری قبلا با این موبایل ثبت شده است.");

            if (await _readService.ExistsByUserNameAsync(request.Username, request.Id))
                throw new BusinessException("کاربری قبلا با این نام ثبت شده است.");

            var user = await _userRepository.GetAsync(t => t.Id == request.Id);
            if( user is null)
                throw new BusinessException("کاربر یافت نشد.");

            user = _factory.Update(user, request.Username, request.PhoneNumber, request.Password, request.FirstName, request.LastName);
            await _userRepository.UpdateAsync(user);
        }
    }
}
