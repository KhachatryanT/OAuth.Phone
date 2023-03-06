using MediatR;

namespace OAuth.Phone.UseCases;

public interface IQuery<out T> : IRequest<T>
{
}