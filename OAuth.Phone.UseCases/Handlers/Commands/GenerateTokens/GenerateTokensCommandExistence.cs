using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;

namespace OAuth.Phone.UseCases.Handlers.Commands.GenerateTokens;

[UsedImplicitly]
internal sealed class GenerateTokensCommandExistence : IVerifyExistence<GenerateTokensCommand>
{
	private readonly IDbContext _dbContext;

	public GenerateTokensCommandExistence(IDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<bool> IsExistsAsync(GenerateTokensCommand request, CancellationToken cancellationToken)
	{
		var userAuthentications = await _dbContext.UserAuthentications
			.Where(x => x.AuthenticationCode == request.Code)
			.ToArrayAsync(cancellationToken);
		return userAuthentications.Length != 1;
	}
}