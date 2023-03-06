using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;
using OAuth.Phone.Infrastructure.Interfaces.Services;
using OAuth.Phone.Utils;

namespace OAuth.Phone.UseCases.Handlers.Commands.GenerateTokens;

[UsedImplicitly]
public sealed class GenerateTokensCommandValidator : AbstractValidator<GenerateTokensCommand>
{
	public GenerateTokensCommandValidator(IAuthCodeProtector authCodeProtector, IDbContext dbContext)
	{
		ClassLevelCascadeMode = CascadeMode.Stop;

		// todo validate ClientId redirectUri validator
		
		RuleFor(req => req.GrantType)
			.Equal("authorization_code")
			.WithMessage(string.Format(ErrorCodes.InvalidParam, nameof(GenerateTokensCommand.GrantType)));

		RuleFor(req => req.Code)
			.NotEmpty()
			.WithMessage(string.Format(ErrorCodes.InvalidParamFormat, nameof(GenerateTokensCommand.Code)));
		
		RuleFor(req => req.CodeVerifier)
			.NotEmpty()
			.WithMessage(string.Format(ErrorCodes.InvalidParamFormat, nameof(GenerateTokensCommand.CodeVerifier)));
		
		RuleFor(req => req)
			.Must(request =>
			{
				var auth = authCodeProtector.Unprotect(request.Code!);
				return auth.Expiry > DateTime.Now || auth.CodeChallenge == ComputeCodeChallenge(request.CodeVerifier!);
			})
			.WithMessage(string.Format(ErrorCodes.InvalidParam,
				$"{nameof(GenerateTokensCommand.Code)}, {nameof(GenerateTokensCommand.CodeVerifier)}"));

		RuleFor(req => req.CodeVerifier)
			.MustAsync(async (code, cancellationToken) =>
			{
				var userAuthentication =
					await dbContext.UserAuthentications.SingleOrDefaultAsync(x => x.AuthenticationCode == code,
						cancellationToken);
				return userAuthentication is not null && !userAuthentication.IsAuthenticationCodeUsed;
			})
			.WithMessage(ErrorCodes.AuthenticationCodeNotFoundOrWasUsed);

		RuleFor(req => req.CodeVerifier)
			.MustAsync(async (code, cancellationToken) =>
			{
				var userAuthentication =
					await dbContext.UserAuthentications.SingleAsync(x => x.AuthenticationCode == code,
						cancellationToken);
				return userAuthentication.AuthenticationCodeExpiration > DateTime.Now;
			})
			.WithMessage(ErrorCodes.AuthenticationCodeExpired);

		RuleFor(req => req.CodeVerifier)
			.MustAsync(async (code, cancellationToken) =>
			{
				var userAuthentication =
					await dbContext.UserAuthentications
						.Include(x=>x.User)
						.SingleAsync(x => x.AuthenticationCode == code,
						cancellationToken);

				return !userAuthentication.User.IdDisabled;
			})
			.WithMessage(ErrorCodes.UserIsDisabled);
	}

	private static string ComputeCodeChallenge(string codeVerifier)
	{
		using var sha256 = SHA256.Create();
		return Base64UrlEncoder.Encode(sha256.ComputeHash(Encoding.ASCII.GetBytes(codeVerifier)));
	}
}