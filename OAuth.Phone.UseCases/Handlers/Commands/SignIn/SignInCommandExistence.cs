using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;

namespace OAuth.Phone.UseCases.Handlers.Commands.SignIn;

[UsedImplicitly]
internal sealed class SignInCommandExistence : IVerifyExistence<SignInCommand>
{
	private readonly IDbContext _dbContext;

	public SignInCommandExistence(IDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<bool> IsExistsAsync(SignInCommand request, CancellationToken cancellationToken)
	{
		return await _dbContext.Users.AnyAsync(x => x.Phone == request.Phone, cancellationToken);
	}
}