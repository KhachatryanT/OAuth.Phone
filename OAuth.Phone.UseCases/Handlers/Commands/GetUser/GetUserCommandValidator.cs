using FluentValidation;
using JetBrains.Annotations;

namespace OAuth.Phone.UseCases.Handlers.Commands.GetUser;

[UsedImplicitly]
public sealed class GetUserCommandValidator : AbstractValidator<GetUserCommand>
{
	public GetUserCommandValidator()
	{
		ClassLevelCascadeMode = CascadeMode.Stop;
		
		
		// todo access token существует и не просрочен
		// пользователь на disabled
		// todo userAgent validate
	}
}