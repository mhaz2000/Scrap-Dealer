using MediatR;

namespace ScrapDealer.Shared.Abstractions.Commands
{
    public interface ICommand : IRequest;
    public interface ICommand<TResponse> : IRequest<TResponse>;
}
