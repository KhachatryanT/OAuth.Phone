using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;

namespace OAuth.Phone.UseCases.Handlers.Commands.GetUser;

[UsedImplicitly]
internal sealed class GetUserCommandHandler : ICommandHandler<GetUserCommand, GetUserCommandResult>
{
	private readonly IDbContext _dbContext;

	public GetUserCommandHandler(IDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<GetUserCommandResult> Handle(GetUserCommand request, CancellationToken cancellationToken)
	{
		// todo получить UserId из JWT AccessToken, а не маппингом из БД. Мб тогда AccessToken вообще не хранить????
		var userAuthentication = _dbContext.UserAuthentications.Single(x => x.AccessToken == request.AccessToken);
		var user = await _dbContext.Users.SingleAsync(x => x.Id == userAuthentication.UserId, cancellationToken);
		return new GetUserCommandResult(user.Phone);
	}
}