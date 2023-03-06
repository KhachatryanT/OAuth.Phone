namespace OAuth.Phone.Entities.Models;

public sealed class UserAuthentication
{
	public int Id { get; init; }
	
	public int UserId { get; init; }
	public User User { get; init; } = default!;

	public string? AuthenticationCode { get; set; }
	public bool IsAuthenticationCodeUsed { get; set; }
	public DateTimeOffset? AuthenticationCodeExpiration { get; set; }
	
	[Obsolete("Dont store it. Only validate JWT AccessToken")]
	public string? AccessToken { get; set; }
	public DateTimeOffset? AccessTokenExpiration { get; set; }
	
	public string? RefreshToken { get; set; }
	public DateTimeOffset? RefreshTokenExpiration { get; set; }
}