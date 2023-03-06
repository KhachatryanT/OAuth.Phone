
using OAuth.Phone.Entities.Models;

namespace OAuth.Phone.Infrastructure.Interfaces.Services;

public interface IAccessTokenGenerator
{
	GeneratedToken Next();
}