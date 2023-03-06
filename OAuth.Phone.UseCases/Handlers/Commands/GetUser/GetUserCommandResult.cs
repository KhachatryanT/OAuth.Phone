using OAuth.Phone.Entities.Models;

namespace OAuth.Phone.UseCases.Handlers.Commands.GetUser;

public sealed class GetUserCommandResult
{
	public GetUserCommandResult(string phone)
	{
		Phone = phone;
	}

	public string Phone { get; }
}