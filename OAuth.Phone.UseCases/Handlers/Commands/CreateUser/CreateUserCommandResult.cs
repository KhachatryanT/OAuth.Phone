using OAuth.Phone.Entities.Models;

namespace OAuth.Phone.UseCases.Handlers.Commands.CreateUser;

public sealed class CreateUserCommandResult
{
	public CreateUserCommandResult(User user)
	{
		User = user;
	}

	public User User { get; }
}