using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.NewsCommand.Handlers
{
    public class DeleteNewsCommandHandler : ICommandHandler<DeleteNewsCommand>
    {
        private readonly INewsRepository _repository;
        public DeleteNewsCommandHandler(INewsRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _repository.GetAsync(c => c.Id == request.Id);
            if (news is null)
                throw new BusinessException("خبر یافت نشد.");

            await _repository.DeleteAsync(news.Id);
        }
    }
}
