namespace OAuth.Phone.UseCases.Handlers.Commands.GetUser;

public sealed class GetUserCommand : ICommand<GetUserCommandResult>
{
	public GetUserCommand(string accessToken)
	{
		AccessToken = accessToken;
	}

	public string AccessToken { get; }
}