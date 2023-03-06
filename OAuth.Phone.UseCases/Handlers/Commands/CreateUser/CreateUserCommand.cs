using JetBrains.Annotations;

namespace OAuth.Phone.UseCases.Handlers.Commands.CreateUser;

[UsedImplicitly]
public sealed class CreateUserCommand: ICommand<CreateUserCommandResult>
{
	public CreateUserCommand(string phone)
	{
		Phone = phone;
	}

	public string Phone { get; }
}