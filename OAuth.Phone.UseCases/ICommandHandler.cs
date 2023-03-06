using MediatR;

namespace OAuth.Phone.UseCases;

internal interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
	where TCommand : ICommand
{
}