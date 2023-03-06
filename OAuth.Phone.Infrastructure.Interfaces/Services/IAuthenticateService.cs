using OAuth.Phone.Entities.Models;

namespace OAuth.Phone.Infrastructure.Interfaces.Services;

public interface IAuthenticateService
{
	Task SignInAsync(User user, string authenticationScheme);
	Task SignOutAsync();
}