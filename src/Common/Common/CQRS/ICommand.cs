using MediatR;

namespace Common.CQRS
{

    public interface IComman : ICommand<Unit>
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
