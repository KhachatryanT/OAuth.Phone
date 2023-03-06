namespace OAuth.Phone.Api.ViewModel;

public sealed class LoginViewModel
{
	public string? RedirectUrl { get; set; }
	public string? Phone { get; set; }
	public int Code { get; set; }
}