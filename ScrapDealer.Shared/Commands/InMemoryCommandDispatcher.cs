using ScrapDealer.Shared.Abstractions.Commands;
using MediatR;

namespace ScrapDealer.Shared.Commands
{
    internal sealed class InMemoryCommandDispatcher : ICommandDispatcher
    {
        private readonly IMediator _mediator;

        public InMemoryCommandDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAsync<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            await _mediator.Send(command);
        }

        public async Task<TResponse> DispatchAsync<TCommand, TResponse>(TCommand command)
            where TCommand : class, ICommand<TResponse>
        {
            return await _mediator.Send(command);
        }
    }
}
