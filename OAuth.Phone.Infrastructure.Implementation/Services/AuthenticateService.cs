using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using OAuth.Phone.Entities.Models;
using OAuth.Phone.Infrastructure.Interfaces.Services;

namespace OAuth.Phone.Infrastructure.Implementation.Services;

internal class AuthenticateService : IAuthenticateService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AuthenticateService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task SignInAsync(User user, string authenticationScheme)
	{
		// Если чувак захочет зайти под другим аккаунтом?
		// Нужно разавторизовывать через 1 мин? или использовать не куки аутентификацию, а bearer? ведь только для связке authentication-login-authentication она нужна
		await _httpContextAccessor.HttpContext.SignInAsync(
			new ClaimsPrincipal(
				new ClaimsIdentity(
					new Claim[]
					{
						new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
					},
					authenticationScheme))
		);
	}

	public Task SignOutAsync()
	{
		throw new NotImplementedException();
	}
}