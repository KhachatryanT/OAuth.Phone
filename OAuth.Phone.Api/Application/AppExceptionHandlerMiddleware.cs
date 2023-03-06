using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OAuth.Phone.Utils;

namespace OAuth.Phone.Api.Application;

internal static class AppExceptionHandlerMiddleware
{
	public static void UseAppExceptionHandler(this IApplicationBuilder app, IHostEnvironment environment)
	{
		app.UseExceptionHandler(builder =>
		{
			if (environment.IsDevelopment())
			{
				builder.Run(WriteDevelopmentResponse);
			}
			else
			{
				builder.Run(WriteProductionResponse);
			}
		});
	}

	private static Task WriteDevelopmentResponse(HttpContext httpContext)
		=> WriteResponse(httpContext, includeDetails: true);

	private static Task WriteProductionResponse(HttpContext httpContext)
		=> WriteResponse(httpContext, includeDetails: false);

	private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
	{
		var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
		var ex = exceptionDetails?.Error;
		if (ex is null)
		{
			return;
		}

		var title = includeDetails
			? "An error occured: " + ex.Message
			: "An error occured";

		var details = includeDetails
			? ex.ToString()
			: null;

		var statusCode = DetermineStatusCode(ex);
		var problem = new ProblemDetails
		{
			Status = statusCode,
			Title = title,
			Detail = details
		};

		// This is often very handy information for tracing the specific request
		var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
		problem.Extensions["traceId"] = traceId;

		// ProblemDetails has it's own content type
		httpContext.Response.ContentType = "application/problem+json";
		httpContext.Response.StatusCode = statusCode;

		await System.Text.Json.JsonSerializer.SerializeAsync(httpContext!.Response.Body, problem);
	}

	private static int DetermineStatusCode(Exception e) => e switch
	{
		NotFoundException => StatusCodes.Status404NotFound,
		BadRequestException => StatusCodes.Status400BadRequest,
		UnauthorizedAccessException => StatusCodes.Status403Forbidden,
		_ => StatusCodes.Status500InternalServerError
	};
}