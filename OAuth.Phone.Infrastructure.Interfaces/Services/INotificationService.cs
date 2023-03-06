namespace OAuth.Phone.Infrastructure.Interfaces.Services;

public interface INotificationService
{
	Task NotifyAsync(string phone, int code, CancellationToken cancellationToken);
}