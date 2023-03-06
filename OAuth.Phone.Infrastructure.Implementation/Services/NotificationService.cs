using OAuth.Phone.Infrastructure.Interfaces.Services;

namespace OAuth.Phone.Infrastructure.Implementation.Services;

internal sealed class NotificationService : INotificationService
{
	public Task NotifyAsync(string phone, int code, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
		throw new NotImplementedException();
	}
}