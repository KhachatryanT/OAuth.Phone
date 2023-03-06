using MediatR;

namespace OAuth.Phone.UseCases;

internal interface ICommand<out T> : IRequest<T>
{

}