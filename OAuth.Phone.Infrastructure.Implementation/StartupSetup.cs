using Microsoft.Extensions.DependencyInjection;
using OAuth.Phone.Infrastructure.Implementation.Services;
using OAuth.Phone.Infrastructure.Interfaces.Services;

namespace OAuth.Phone.Infrastructure.Implementation;

public static class StartupSetup
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddSingleton<DevKeys>();
		services.AddScoped<IAccessTokenGenerator, AccessTokenGenerator>();
		services.AddScoped<IAuthCodeProtector, AuthCodeProtector>();
		services.AddScoped<IAuthenticateService, AuthenticateService>();
		services.AddScoped<INotificationService, NotificationService>();
		services.AddScoped<IIdentityUserAccessor, IdentityUserAccessor>();
		return services;
	}
}