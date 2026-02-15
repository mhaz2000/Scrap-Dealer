using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.NewsCommand.Handlers
{
    public class AddNewsCommandHandler : ICommandHandler<AddNewsCommand>
    {
        private readonly INewsFactory _factory;
        private readonly INewsRepository _repository;
        public AddNewsCommandHandler(INewsFactory factory, INewsRepository repository)
        {
            _repository = repository;
            _factory = factory;
        }

        public async Task Handle(AddNewsCommand request, CancellationToken cancellationToken)
        {
            var news = _factory.Create(request.title, request.summary, request.content);

            await _repository.AddAsync(news);
        }
    }
}
