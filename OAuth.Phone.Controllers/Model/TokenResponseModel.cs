using System.Text.Json.Serialization;

namespace OAuth.Phone.Controllers.Model;

public sealed class TokenResponseModel
{
	[JsonPropertyName("access_token")]
	public string?  AccessToken { get; set; }

	[JsonPropertyName("token_type")]
	public string? TokenType { get; set; }

	/// <remarks>
	/// In seconds
	/// </remarks>
	[JsonPropertyName("expires_in")]
	public int ExpiresIn { get; set; }
	
	[JsonPropertyName("refresh_token")]
	public int RefreshToken { get; set; }
}