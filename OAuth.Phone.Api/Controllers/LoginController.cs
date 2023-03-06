using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using OAuth.Phone.Api.ViewModel;
using OAuth.Phone.UseCases.Handlers.Commands.SignIn;

namespace OAuth.Phone.Api.Controllers;

[Route("[controller]")]
public class LoginController : Controller
{
	private readonly ISender _sender;

	public LoginController(ISender sender)
	{
		_sender = sender;
	}

	[HttpGet]
	public IActionResult Index()
	{
		Request.Query.TryGetValue("RedirectUrl", out var redirectUrl);

		if (!IsRedirectUrlPermitted(redirectUrl))
		{
			return BadRequest();
		}

		return View(new LoginViewModel { RedirectUrl = redirectUrl });
	}


	[HttpPost]
	public async Task<IActionResult> Index([FromForm] LoginViewModel model)
	{
		if (!IsRedirectUrlPermitted(model.RedirectUrl))
		{
			return BadRequest();
		}

		await _sender.Send(new SignInCommand(model.Phone, model.Code), HttpContext.RequestAborted);
		return Redirect(model.RedirectUrl);
	}

	private bool IsRedirectUrlPermitted(string? redirectUrl)
	{
		if (string.IsNullOrEmpty(redirectUrl))
		{
			return false;
		}

		if (IsRelativeUrl(redirectUrl))
		{
			return true;
		}

		var currentPageUriAuthority = new Uri(Request.GetEncodedUrl()).GetLeftPart(UriPartial.Authority);
		return redirectUrl.StartsWith(currentPageUriAuthority, StringComparison.OrdinalIgnoreCase);
	}

	private static bool IsRelativeUrl(string url) => url.StartsWith("/") || url.StartsWith("~/");
}