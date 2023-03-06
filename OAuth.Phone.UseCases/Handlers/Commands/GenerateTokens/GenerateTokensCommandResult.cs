namespace OAuth.Phone.UseCases.Handlers.Commands.GenerateTokens;

public sealed class GenerateTokensCommandResult
{
	public GenerateTokensCommandResult(string accessToken, int expiresInSec)
	{
		AccessToken = accessToken;
		ExpiresInSec = expiresInSec;
	}

	public string AccessToken { get; }
	public int ExpiresInSec { get; }

	public string? RefreshToken { get; init; }
}