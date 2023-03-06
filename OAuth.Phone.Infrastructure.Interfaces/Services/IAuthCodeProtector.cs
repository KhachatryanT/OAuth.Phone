using OAuth.Phone.Entities.Models;

namespace OAuth.Phone.Infrastructure.Interfaces.Services;

public interface IAuthCodeProtector
{
	string Protect(AuthCode authCode);
	AuthCode Unprotect(string code);
}