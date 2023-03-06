namespace OAuth.Phone.Utils.Settings;

public sealed class TokenSettings
{
	/// <summary>
	/// Access token TTL
	/// </summary>
	public TimeSpan AccessTokenExpiration { get; init; }
	
	/// <summary>
	/// Refresh token TTL
	/// </summary>
	public TimeSpan RefreshTokenExpiration { get; init; }
	
	public static string Section => "Token";
}