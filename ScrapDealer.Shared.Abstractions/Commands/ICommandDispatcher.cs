namespace ScrapDealer.Shared.Abstractions.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
        Task<TResponse> DispatchAsync<TCommand, TResponse>(TCommand command) where TCommand : class, ICommand<TResponse>;
    }
}
