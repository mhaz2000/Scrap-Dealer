using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Supports.Handlers
{
    internal class DeleteSupportHandler(IUserRepository _userRepository) : ICommandHandler<DeleteSupportCommand>
    {
        public async Task Handle(DeleteSupportCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(t => t.Id == request.Id);
            if (user is null)
                throw new BusinessException("کاربر یافت نشد.");

            await _userRepository.DeleteAsync(request.Id);
        }
    }
}
