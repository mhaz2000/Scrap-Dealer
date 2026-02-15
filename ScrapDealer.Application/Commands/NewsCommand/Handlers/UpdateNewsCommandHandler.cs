using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.NewsCommand.Handlers
{
    public class UpdateNewsCommandHandler : ICommandHandler<UpdateNewsCommand>
    {
        private readonly INewsFactory _factory;
        private readonly INewsRepository _repository;
        public UpdateNewsCommandHandler(INewsFactory factory, INewsRepository repository)
        {
            _repository = repository;
            _factory = factory;
        }

        public async Task Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _repository.GetAsync(c => c.Id == request.id);
            if (news is null)
                throw new BusinessException("خبر یافت نشد.");

            news = _factory.Update(request.title, request.summary, request.content, news);
            await _repository.UpdateAsync(news);
        }
    }
}
