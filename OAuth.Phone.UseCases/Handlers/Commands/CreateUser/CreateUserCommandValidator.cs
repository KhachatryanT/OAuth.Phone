using FluentValidation;
using JetBrains.Annotations;

namespace OAuth.Phone.UseCases.Handlers.Commands.CreateUser;

[UsedImplicitly]
public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator()
	{
		ClassLevelCascadeMode = CascadeMode.Stop;
		
		// todo phone is valid via regexp
		// todo user not exists
	}
}