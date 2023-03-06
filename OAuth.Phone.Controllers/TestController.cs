using Microsoft.AspNetCore.Mvc;
using OAuth.Phone.Utils;

namespace OAuth.Phone.Controllers;

[Route("[controller]")]
public class TestController: ControllerBase
{
	[HttpGet("[action]")]
	public IActionResult Exception()
	{
		throw new NotFoundException();
	}
}