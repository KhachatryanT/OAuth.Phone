using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
using OAuth.Phone.Entities.Models;
using OAuth.Phone.Infrastructure.Interfaces.Services;
using OAuth.Phone.Utils;

namespace OAuth.Phone.Infrastructure.Implementation.Services;

internal sealed class AuthCodeProtector : IAuthCodeProtector
{
	private readonly IDataProtectionProvider _dataProtectionProvider;

	public AuthCodeProtector(IDataProtectionProvider dataProtectionProvider)
	{
		_dataProtectionProvider = dataProtectionProvider;
	}

	public string Protect(AuthCode authCode)
	{
		var protector = _dataProtectionProvider.CreateProtector(Defaults.ProtectionPurpose);
		return protector.Protect(JsonSerializer.Serialize(authCode));
	}

	public AuthCode Unprotect(string code)
	{
		var protector = _dataProtectionProvider.CreateProtector(Defaults.ProtectionPurpose);
		var codeString = protector.Unprotect(code);
		return JsonSerializer.Deserialize<AuthCode>(codeString) ??
		       throw new ArgumentNullException("Десериализация в null");
	}
}