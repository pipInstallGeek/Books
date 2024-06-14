using MediatR;

namespace Books.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
    {
    }
}
