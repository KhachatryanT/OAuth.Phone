namespace OAuth.Phone.Utils.Settings;

public sealed class ConfirmationCodeSettings
{
	/// <summary>
	/// TTL кода подтверждения
	/// </summary>
	public TimeSpan Expiration { get; init; }
	
	/// <summary>
	/// Количество ошибок ввода кода подтверждения
	/// </summary>
	public int ConfirmationErrorsCount { get; init; }
	
	/// <summary>
	/// Интервал запросов кода подтверждения
	/// </summary>
	public TimeSpan RequestConfirmationCodeInterval { get; init; }

	public static string Section => "ConfirmationCode";

}