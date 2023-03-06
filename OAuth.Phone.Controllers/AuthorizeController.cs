using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using OAuth.Phone.UseCases.Handlers.Commands.GenerateAuthorizationCode;
using OAuth.Phone.Utils;

namespace OAuth.Phone.Controllers;

[Route("[controller]")]
public class AuthorizeController : ControllerBase
{
	private readonly ISender _sender;

	public AuthorizeController(ISender sender)
	{
		_sender = sender;
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Index()
	{
		Request.Query.TryGetValue("client_id", out var clientId);
		Request.Query.TryGetValue("code_challenge", out var codeChallenge);
		Request.Query.TryGetValue("code_challenge_method", out var codeChallengeMethod);
		Request.Query.TryGetValue("redirect_uri", out var redirectUri);
		Request.Query.TryGetValue("scope", out var scope);
		Request.Query.TryGetValue("state", out var state);

		var generatedResult = await _sender.Send(new GenerateAuthorizationCodeCommand
		{
			ClientId = clientId,
			CodeChallenge = codeChallenge,
			RedirectUri = redirectUri,
			CodeChallengeMethod = codeChallengeMethod
		});

		var qs = new Dictionary<string, string>()
		{
			{ "code", generatedResult.Code },
			{ "state", state },
			{ "iss", Defaults.Issuer }
		};

		return Redirect(QueryHelpers.AddQueryString(redirectUri, qs));
	}
}