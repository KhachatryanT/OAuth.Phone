using MediatR;
using Microsoft.AspNetCore.Mvc;
using OAuth.Phone.Controllers.Model;
using OAuth.Phone.UseCases.Handlers.Commands.GenerateTokens;

namespace OAuth.Phone.Controllers;

[Route("[controller]")]
public class TokenController : ControllerBase
{
	private readonly ISender _sender;

	public TokenController(ISender sender)
	{
		_sender = sender;
	}

	[HttpPost]
	public async Task<TokenResponseModel> Index(TokenRequestModel request)
	{
		var result = await _sender.Send(new GenerateTokensCommand
		{
			Code = request.Code,
			ClientId = request.ClientId,
			CodeVerifier = request.CodeVerifier,
			GrantType = request.GrantType,
			RedirectUri = request.RedirectUri,
		}, HttpContext.RequestAborted);

		return new TokenResponseModel
		{
			AccessToken = result.AccessToken,
			ExpiresIn = result.ExpiresInSec,
			TokenType = "Bearer"
		};
	}
}