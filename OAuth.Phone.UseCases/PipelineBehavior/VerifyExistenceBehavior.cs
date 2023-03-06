using MediatR;
using OAuth.Phone.Utils;

namespace OAuth.Phone.UseCases.PipelineBehavior;

internal sealed class VerifyExistenceBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
	where TRequest : IRequest<TResult>
{
	private readonly IEnumerable<IVerifyExistence<TRequest>> _validators;

	public VerifyExistenceBehavior(IEnumerable<IVerifyExistence<TRequest>> validators)
	{
		_validators = validators;
	}

	public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next,
		CancellationToken cancellationToken)
	{
		if (!_validators.Any())
		{
			return await next();
		}

		var isExists = await _validators
			.ToAsyncEnumerable()
			.SelectAwaitWithCancellation(async (x, c) => await x.IsExistsAsync(request, c))
			.AllAsync(x => x, cancellationToken);

		if (!isExists)
		{
			throw new NotFoundException(ErrorCodes.PhoneNotFound);
		}

		return await next();
	}
}