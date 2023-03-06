using MediatR;

namespace OAuth.Phone.UseCases;

internal interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
	where TQuery : IQuery<TResult>
{
}