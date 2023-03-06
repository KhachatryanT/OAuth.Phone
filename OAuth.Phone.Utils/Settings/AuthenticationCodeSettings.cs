namespace OAuth.Phone.Utils.Settings;

public sealed class AuthenticationCodeSettings
{
	/// <summary>
	/// TTL кода авторизации
	/// </summary>
	public TimeSpan Expiration { get; init; }
	public static string Section => "AuthenticationCode";
}