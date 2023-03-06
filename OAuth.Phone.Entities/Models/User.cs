namespace OAuth.Phone.Entities.Models;

public class User 
{
	// ReSharper disable once UnusedAutoPropertyAccessor.Global
	public int Id { get; init; }

	public string Phone { get; init; } = default!;
	
	public bool IdDisabled { get; init; }
	
	/// <summary>
	/// Код подтверждения
	/// </summary>
	public int? ConfirmationCode { get; set; }

	/// <summary>
	/// Код подтверждения действителен до
	/// </summary>
	public DateTimeOffset? ConfirmationCodeAvailableUntil { get; set; }
	
	/// <summary>
	/// Количество ошибок, допущенных при вводе кода подтверждения
	/// </summary>
	public int ConfirmationErrorsCount { get; set; }
	
	/// <summary>
	/// Дата следующей отправки кода подтверждения
	/// </summary>
	public DateTimeOffset? NextRequestConfirmationCodeAvailableAt { get; set; }

	/// <summary>
	/// Дата последней авторизации
	/// </summary>
	public DateTimeOffset? LastSignIn { get; set; }
}