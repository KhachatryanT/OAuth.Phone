using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OAuth.Phone.Entities.Models;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;
using OAuth.Phone.Infrastructure.Interfaces.Services;

namespace OAuth.Phone.Infrastructure.Implementation.Services;

internal sealed class IdentityUserAccessor : IIdentityUserAccessor
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public IdentityUserAccessor(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false;

	public int UserId
	{
		get
		{
			if (!IsAuthenticated)
			{
				throw new UnauthorizedAccessException();
			}

			return int.Parse(_httpContextAccessor.HttpContext.User.Claims
				                 .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
			                 ?? "0");
		}
	}
}