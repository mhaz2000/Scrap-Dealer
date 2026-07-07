using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Sellers.Handlers
{
    internal class SellerRemoveAccountHandler(ISellerRepository sellerRepository, ISaleOrderReadService saleOrderReadService,
        ISaleOrderRequestReadService saleOrderRequestReadService) : ICommandHandler<SellerRemoveAccountCommand>
    {
        public async Task Handle(SellerRemoveAccountCommand request, CancellationToken cancellationToken)
        {
            var seller = await sellerRepository.GetAsync(t => t.UserId == request.Id);
            if (seller is null)
                throw new BusinessException("فروشنده یافت نشد.");

            if(await saleOrderReadService.HasOngoingContractForSaleorderAsync(seller.Id))
                throw new BusinessException("برای حذف حساب کاربری ابتدا قرارداد های خود را تعیین تکلیف نمایید.");

            if (await saleOrderRequestReadService.HasOngoingSaleOrderRequest(seller.Id))
                throw new BusinessException("برای حذف حساب کاربری ابتدا وضعیت درخواست های فروش به خریداران را تعیین تکلیف نمایید.");

            await sellerRepository.DeleteAsync(seller.Id);

            await sellerRepository.CommitAsync();
        }
    }
}