namespace OAuth.Phone.Infrastructure.Interfaces.Services;

public interface IIdentityUserAccessor
{
	bool IsAuthenticated { get; }
	int UserId { get; }
}