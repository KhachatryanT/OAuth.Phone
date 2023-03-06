using MediatR;
using Microsoft.AspNetCore.Mvc;
using OAuth.Phone.UseCases.Handlers.Commands.SendConfirmationCode;

namespace OAuth.Phone.Controllers.Api;

[Route("api/confirmation")]
public class ConfirmationController : ControllerBase
{
	private readonly ISender _sender;

	public ConfirmationController(ISender sender)
	{
		_sender = sender;
	}

	/// <summary>
	/// Отправить код подтверждения
	/// </summary>
	/// <param name="phone"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	[HttpPost("send")]
	public async Task<IActionResult> SendConfirmationCode(string phone)
	{
		await _sender.Send(new SendConfirmationCodeCommand(phone), HttpContext.RequestAborted);
		return Ok();
	}
}