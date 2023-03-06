using FluentValidation;
using FluentValidation.Validators;
using JetBrains.Annotations;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;
using OAuth.Phone.UseCases.Handlers.CommonValidators;
using OAuth.Phone.Utils;

namespace OAuth.Phone.UseCases.Handlers.Commands.SignIn;

[UsedImplicitly]
public sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
{
	public SignInCommandValidator(IDbContext dbContext)
	{
		ClassLevelCascadeMode = CascadeMode.Stop;
		
		RuleFor(req => req.Phone)
			.NotEmpty()
			.WithMessage(string.Format(ErrorCodes.InvalidParamFormat, nameof(SignInCommand.Phone)));

		RuleFor(req => req.Phone)
			.SetAsyncValidator(new IsUserDisabledValidator<SignInCommand>(dbContext))
			.WithMessage(ErrorCodes.UserIsDisabled);

		// todo user existence validator
		// todo confirmationCode > 0
		// todo confirmationCode expiration validator
		// todo confirmationCode errors input validator

	}
}