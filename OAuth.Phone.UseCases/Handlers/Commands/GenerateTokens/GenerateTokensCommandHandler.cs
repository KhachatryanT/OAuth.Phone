using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;
using OAuth.Phone.Infrastructure.Interfaces.Services;

namespace OAuth.Phone.UseCases.Handlers.Commands.GenerateTokens;

[UsedImplicitly]
internal sealed class GenerateTokensCommandHandler : ICommandHandler<GenerateTokensCommand, GenerateTokensCommandResult>
{
	private readonly IAccessTokenGenerator _accessTokenGenerator;
	private readonly IDbContext _dbContext;

	public GenerateTokensCommandHandler(IAccessTokenGenerator accessTokenGenerator, IDbContext dbContext)
	{
		_accessTokenGenerator = accessTokenGenerator;
		_dbContext = dbContext;
	}

	public async Task<GenerateTokensCommandResult> Handle(GenerateTokensCommand request,
		CancellationToken cancellationToken)
	{
		var now = DateTimeOffset.Now;
		var userAuthentication =
			await _dbContext.UserAuthentications
				.SingleAsync(x => x.AuthenticationCode == request.Code, cancellationToken);

		var (accessToken, expiration) = _accessTokenGenerator.Next();
		userAuthentication.AccessToken = accessToken;
		userAuthentication.AccessTokenExpiration = now.Add(expiration);
		userAuthentication.IsAuthenticationCodeUsed = true;

		await _dbContext.SaveChangesAsync(cancellationToken);

		return new GenerateTokensCommandResult(accessToken, (int)expiration.TotalSeconds);
	}
}