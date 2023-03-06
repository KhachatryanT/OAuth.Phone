using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using OAuth.Phone.Entities.Models;
using OAuth.Phone.Infrastructure.Interfaces.Services;
using OAuth.Phone.Utils.Settings;

namespace OAuth.Phone.Infrastructure.Implementation.Services;

internal class AccessTokenGenerator : IAccessTokenGenerator
{
	private readonly DevKeys _devKeys;
	private readonly IOptions<TokenSettings> _tokenOptions;

	public AccessTokenGenerator(DevKeys devKeys, IOptions<TokenSettings> tokenOptions)
	{
		_devKeys = devKeys;
		_tokenOptions = tokenOptions;
	}

	public GeneratedToken Next()
	{
		var ttl = _tokenOptions.Value.AccessTokenExpiration;
		var token =  new JsonWebTokenHandler().CreateToken(new SecurityTokenDescriptor()
		{
			Claims = new Dictionary<string, object>()
			{
				{ "auth_way", "phone" },
			},
			Issuer = "Phone",
			Expires = DateTime.Now.Add(ttl),
			TokenType = "Bearer",
			SigningCredentials = new(_devKeys.RsaSecurityKey, SecurityAlgorithms.RsaSha256)
		});
		return new GeneratedToken(token, ttl);
	}
	
}