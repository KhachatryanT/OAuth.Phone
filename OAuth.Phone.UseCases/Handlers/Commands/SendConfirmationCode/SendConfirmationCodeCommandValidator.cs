using FluentValidation;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;
using OAuth.Phone.UseCases.Handlers.Commands.CreateUser;
using OAuth.Phone.Utils;

namespace OAuth.Phone.UseCases.Handlers.Commands.SendConfirmationCode;

[UsedImplicitly]
public sealed class SendConfirmationCodeCommandValidator : AbstractValidator<CreateUserCommand>
{
	public SendConfirmationCodeCommandValidator(IDbContext dbContext)
	{
		ClassLevelCascadeMode = CascadeMode.Stop;

		RuleFor(req => req.Phone)
			.MustAsync(async (phone, cancellationToken) =>
			{
				var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Phone == phone, cancellationToken);
				if (user is null)
				{
					return true;
				}

				return !user.IdDisabled;
			})
			.WithMessage(ErrorCodes.UserIsDisabled);

		RuleFor(req => req.Phone)
			.MustAsync(async (phone, cancellationToken) =>
			{
				var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Phone == phone, cancellationToken);
				if (user is null)
				{
					return true;
				}

				return !user.NextRequestConfirmationCodeAvailableAt.HasValue ||
				       user.NextRequestConfirmationCodeAvailableAt <= DateTimeOffset.Now;
			})
			.WithMessage(ErrorCodes.ConfirmationCodeSendWait);
	}
}