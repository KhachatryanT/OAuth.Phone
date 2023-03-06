using JetBrains.Annotations;

namespace OAuth.Phone.UseCases.Handlers.Commands.SendConfirmationCode;

[UsedImplicitly]
public sealed class SendConfirmationCodeCommand: ICommand
{
	public SendConfirmationCodeCommand(string phone)
	{
		Phone = phone;
	}

	public string Phone { get; }
}