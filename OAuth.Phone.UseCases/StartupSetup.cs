using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OAuth.Phone.UseCases.PipelineBehavior;
using OAuth.Phone.Utils;

namespace OAuth.Phone.UseCases;

public static class StartupSetup
{
	public static IServiceCollection AddUseCases(this IServiceCollection services)
	{
		var assembly = typeof(StartupSetup).Assembly;
		services.AddMediatR(assembly);
		services.AddValidatorsFromAssembly(assembly);
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(VerifyExistenceBehavior<,>));
		services.RegisterAllFromAssignableInterface(typeof(IVerifyExistence<>), assembly);
		return services;
	}
}