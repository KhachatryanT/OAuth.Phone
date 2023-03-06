namespace OAuth.Phone.UseCases.Handlers.Commands.GenerateAuthorizationCode;

public sealed class GenerateAuthorizationCodeCommand: ICommand<GenerateAuthorizationCodeCommandResult>
{
	public string? ClientId { get; init; }
	public string? CodeChallenge { get; init; }
	public string? CodeChallengeMethod { get; init; }
	public string? RedirectUri { get; init; }
}