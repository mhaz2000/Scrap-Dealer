using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Buyers.Handlers
{
    internal class BuyerRemoveAccountHandler(IBuyerRepository buyerRepository, IContractReadService contractReadService) : ICommandHandler<BuyerRemoveAccountCommand>
    {
        public async Task Handle(BuyerRemoveAccountCommand request, CancellationToken cancellationToken)
        {
            var buyer = await buyerRepository.GetAsync(t => t.UserId == request.Id);
            if (buyer is null)
                throw new BusinessException("خریدار یافت نشد.");

            if(await contractReadService.HasOngoingContractAsync(buyer.Id))
                throw new BusinessException("برای حذف حساب کاربری ابتدا قرارداد های خود را تعیین تکلیف نمایید.");

            await buyerRepository.DeleteAsync(buyer.Id);

            await buyerRepository.CommitAsync();

        }
    }
}
