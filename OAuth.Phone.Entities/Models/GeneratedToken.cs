namespace OAuth.Phone.Entities.Models;

public sealed class GeneratedToken
{
	public GeneratedToken(string token, TimeSpan expiration)
	{
		Token = token;
		Expiration = expiration;
	}
	
	public void Deconstruct (out string token, out TimeSpan expiration)
	{
		token = Token;
		expiration = Expiration;
	}

	public string Token { get; }
	public TimeSpan Expiration { get; }
}