using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OAuth.Phone.UseCases.Handlers.Commands.GetUser;

namespace OAuth.Phone.Controllers;

[Route("[controller]")]
public class UserController : ControllerBase
{
	private readonly ISender _sender;

	public UserController(ISender sender)
	{
		_sender = sender;
	}

	[HttpGet]
	// todo accessCode to Authentication ?
	public async Task<ActionResult<GetUserCommandResult>> Index(string accessCode) =>
		await _sender.Send(new GetUserCommand(accessCode));

	// todo Temp. Delete action
	[Obsolete("delete action")]
	[HttpGet("[action]")]
	public IActionResult Info()
	{
		JsonSerializerOptions options = new()
		{
			WriteIndented = true,
			ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
		};
		var serialized = JsonSerializer.Serialize(User, options);
		return Ok(serialized);
	}
}