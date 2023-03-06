using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;
using OAuth.Phone.Utils;

namespace OAuth.Phone.UseCases.Handlers.CommonValidators;

public sealed class IsUserDisabledValidator<T> : AsyncPropertyValidator<T, string?>
	where T : IRequest
{
	private readonly IDbContext _dbContext;

	public IsUserDisabledValidator(IDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public override async Task<bool> IsValidAsync(ValidationContext<T> context, string phone,
		CancellationToken cancellation)
	{
		var user = await _dbContext.Users.SingleAsync(x => x.Phone == phone, cancellation);
		return !user.IdDisabled;
	}

	public override string Name => nameof(IsUserDisabledValidator<T>);
}