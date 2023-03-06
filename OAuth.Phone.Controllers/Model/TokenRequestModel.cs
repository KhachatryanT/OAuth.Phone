using Microsoft.AspNetCore.Mvc;

namespace OAuth.Phone.Controllers.Model;

public sealed class TokenRequestModel
{
	[BindProperty(Name = "grant_type")]
	public string? GrantType { get; set; }
	
	[BindProperty(Name = "client_id")]
	public string? ClientId { get; set; }
	
	[BindProperty(Name = "redirect_uri")]
	public string? RedirectUri { get; set; }
	
	[BindProperty(Name = "code_verifier")]
	public string? CodeVerifier { get; set; }
	
	[BindProperty(Name = "code")]
	public string? Code { get; set; }
}