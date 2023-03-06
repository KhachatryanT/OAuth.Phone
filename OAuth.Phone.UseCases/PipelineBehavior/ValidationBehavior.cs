using FluentValidation;
using MediatR;

namespace OAuth.Phone.UseCases.PipelineBehavior;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> _validators;

	public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
	{
		_validators = validators;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		if (!_validators.Any())
		{
			return await next();
		}

		var context = new ValidationContext<TRequest>(request);

		var errors = await _validators
			.ToAsyncEnumerable()
			.SelectAwaitWithCancellation(async (x, c) => await x.ValidateAsync(context, c))
			.ToArrayAsync(cancellationToken);

		var failures = errors
			.SelectMany(x => x.Errors)
			.Where(x => x is not null)
			.ToArray();

		if (failures.Any())
		{
			throw new ValidationException(failures);
		}

		return await next();
	}
}