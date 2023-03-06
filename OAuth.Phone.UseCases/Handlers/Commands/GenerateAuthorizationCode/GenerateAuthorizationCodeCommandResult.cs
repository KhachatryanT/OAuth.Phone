namespace OAuth.Phone.UseCases.Handlers.Commands.GenerateAuthorizationCode;

public sealed class GenerateAuthorizationCodeCommandResult
{
	public GenerateAuthorizationCodeCommandResult(string code)
	{
		Code = code;
	}

	public string Code { get; }
}