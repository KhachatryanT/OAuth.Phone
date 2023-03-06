namespace OAuth.Phone.UseCases.Handlers.Commands.GenerateTokens;

public sealed class GenerateTokensCommand : ICommand<GenerateTokensCommandResult>
{
	public string? ClientId { get; init; }
	public string? GrantType { get; init; }

	public string? RedirectUri { get; init; }
	public string? CodeVerifier { get; init; }
	public string? Code { get; init; }
}