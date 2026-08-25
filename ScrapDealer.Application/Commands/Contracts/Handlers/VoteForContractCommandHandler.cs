using MediatR;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Events.CategoryPriceHistories;
using ScrapDealer.Domain.Events.ScoreHistories;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Contracts.Handlers;

internal class VoteForContractCommandHandler(IMediator mediator,IContractRepository contractRepository, IScoreHistoryRepository scoreHistoryRepository, IScoreHistoryFactory scoreHistoryFactory)
    : ICommandHandler<VoteForContractCommand>
{
    public async Task Handle(VoteForContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await contractRepository.GetAsync(t => t.Id == request.ContractId, t => t.Include(s => s.Buyer).Include(s => s.SaleOrder).ThenInclude(s => s.Seller));
        if (contract is null)
            throw new BusinessException("قرارداد یافت نشد.");

        if (contract.Status != ContractStatus.Done)
            throw new BusinessException("وضعیت قرارداد هنوز پایان نیافته است.");

        bool isBuyer = contract.Buyer.UserId == request.UserId;
        bool isSeller = contract.SaleOrder.Seller.UserId == request.UserId;

        if(!isSeller && !isBuyer)
            throw new BusinessException("قرداد یافت نشد.");

        var scoreFor = isBuyer ? ScoreFor.Seller : ScoreFor.Buyer;

        var existing = await scoreHistoryRepository.GetAsync(
            sh => sh.ContractId == request.ContractId && sh.ScoreFor == scoreFor);

        if (existing is not null)
        {
            existing.Update(request.Score, request.Comment);
        }
        else
        {
            var scoreHistory = scoreHistoryFactory.Create(request.Score, contract.Buyer, contract.SaleOrder.Seller, contract, scoreFor, request.Comment);
            await scoreHistoryRepository.AddAsync(scoreHistory);
        }

        await scoreHistoryRepository.CommitAsync();

        if(isBuyer)
            await mediator.Publish(new UpdateSellerScoreEvent(contract.SaleOrder.SellerId, request.Score));
        else
            await mediator.Publish(new UpdateBuyerScoreEvent(contract.BuyerId, request.Score));
    }
}

