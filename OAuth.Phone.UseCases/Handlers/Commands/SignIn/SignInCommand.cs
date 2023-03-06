namespace OAuth.Phone.UseCases.Handlers.Commands.SignIn;

public sealed class SignInCommand: ICommand
{
	public SignInCommand(string? phone, int code)
	{
		Phone = phone;
		Code = code;
	}

	public string? Phone { get; }
	public int Code { get; }
}