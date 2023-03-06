using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;

namespace OAuth.Phone.UseCases.Handlers.Commands.GetUser;

[UsedImplicitly]
public class GetUserCommandExistence : IVerifyExistence<GetUserCommand>
{
	private readonly IDbContext _dbContext;

	public GetUserCommandExistence(IDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<bool> IsExistsAsync(GetUserCommand request, CancellationToken cancellationToken)
	{
		// todo получить UserId из JWT AccessToken, а не маппингом из БД. Мб тогда AccessToken вообще не хранить????
		var userAuthentications = await _dbContext.UserAuthentications
			.Where(x => x.AccessToken == request.AccessToken)
			.ToArrayAsync(cancellationToken);

		return userAuthentications.Length == 1 &&
		       await _dbContext.Users.AnyAsync(x => x.Id == userAuthentications[0].UserId, cancellationToken);
	}
}