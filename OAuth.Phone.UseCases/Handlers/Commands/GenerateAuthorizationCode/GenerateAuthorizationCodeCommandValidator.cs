using FluentValidation;
using JetBrains.Annotations;
using OAuth.Phone.UseCases.Handlers.Commands.CreateUser;

namespace OAuth.Phone.UseCases.Handlers.Commands.GenerateAuthorizationCode;

[UsedImplicitly]
internal sealed class GenerateAuthorizationCodeCommandValidator : AbstractValidator<CreateUserCommand>
{
	public GenerateAuthorizationCodeCommandValidator()
	{
		ClassLevelCascadeMode = CascadeMode.Stop;
		
		// todo query response_type" == "code
		// todo validate ClientId redirectUri validator
		// todo User exists
		// todo user is not disabled

	}
}