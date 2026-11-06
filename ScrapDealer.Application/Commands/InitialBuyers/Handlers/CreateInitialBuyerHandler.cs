using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.InitialBuyers.Handlers;

internal sealed class CreateInitialBuyerHandler : ICommandHandler<CreateInitialBuyerCommand>
{
    private readonly IInitialBuyerRepository _repository;

    public CreateInitialBuyerHandler(IInitialBuyerRepository repository) => _repository = repository;

    public async Task Handle(CreateInitialBuyerCommand request, CancellationToken cancellationToken)
    {
        var phone = Phone.Create(request.Phone);
        if (await _repository.GetAsync(b => b.Phone == phone) is not null)
            throw new BusinessException("این شماره تلفن قبلا ثبت شده است.");

        var buyer = new InitialBuyer(PersonName.Create(request.FirstName, request.LastName), phone, request.Area, request.HasVehicle, request.OwnsShop);
        await _repository.AddAsync(buyer);
        await _repository.CommitAsync();
    }
}
