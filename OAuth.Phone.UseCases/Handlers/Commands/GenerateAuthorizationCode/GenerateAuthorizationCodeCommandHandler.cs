using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OAuth.Phone.Entities.Models;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;
using OAuth.Phone.Infrastructure.Interfaces.Services;
using OAuth.Phone.Utils.Settings;

namespace OAuth.Phone.UseCases.Handlers.Commands.GenerateAuthorizationCode;

[UsedImplicitly]
internal sealed class GenerateAuthorizationCodeCommandHandler : ICommandHandler<GenerateAuthorizationCodeCommand,
	GenerateAuthorizationCodeCommandResult>
{
	private readonly IAuthCodeProtector _authCodeProtector;
	private readonly IOptions<AuthenticationCodeSettings> _authenticationCodeSettings;
	private readonly IDbContext _dbContext;
	private readonly IIdentityUserAccessor _identityUserAccessor;

	public GenerateAuthorizationCodeCommandHandler(IAuthCodeProtector authCodeProtector,
		IOptions<AuthenticationCodeSettings> authenticationCodeSettings,
		IDbContext dbContext,
		IIdentityUserAccessor identityUserAccessor)
	{
		_authCodeProtector = authCodeProtector;
		_authenticationCodeSettings = authenticationCodeSettings;
		_dbContext = dbContext;
		_identityUserAccessor = identityUserAccessor;
	}

	public async Task<GenerateAuthorizationCodeCommandResult> Handle(GenerateAuthorizationCodeCommand request,
		CancellationToken cancellationToken)
	{
		var code = _authCodeProtector.Protect(new AuthCode(
			request.ClientId ?? string.Empty,
			request.CodeChallenge ?? string.Empty,
			request.CodeChallengeMethod ?? string.Empty,
			request.RedirectUri ?? string.Empty,
			DateTime.Now.Add(_authenticationCodeSettings.Value.Expiration)));

		var user = await _dbContext.Users.SingleAsync(x => x.Id == _identityUserAccessor.UserId, cancellationToken);
		user.LastSignIn = DateTimeOffset.Now;
		
		_dbContext.UserAuthentications.Add(new UserAuthentication
		{
			UserId = user.Id,
			AuthenticationCode = code,
			AuthenticationCodeExpiration = DateTimeOffset.Now.Add(_authenticationCodeSettings.Value.Expiration),
		});
		await _dbContext.SaveChangesAsync(cancellationToken);

		return new GenerateAuthorizationCodeCommandResult(code);
	}
}