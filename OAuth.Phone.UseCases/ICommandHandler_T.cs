using MediatR;

namespace OAuth.Phone.UseCases;

internal interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
	where TCommand : ICommand<TResult>
{
}