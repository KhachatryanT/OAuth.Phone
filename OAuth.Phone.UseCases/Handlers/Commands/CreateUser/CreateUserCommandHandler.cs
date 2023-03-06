using JetBrains.Annotations;
using OAuth.Phone.Entities.Models;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;

namespace OAuth.Phone.UseCases.Handlers.Commands.CreateUser;

[UsedImplicitly]
internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserCommandResult>
{
	private readonly IDbContext _dbContext;

	public CreateUserCommandHandler(IDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	
	public async Task<CreateUserCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		var user = new User
		{
			Phone = request.Phone
		};
		_dbContext.Users.Add(user);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return new CreateUserCommandResult(user);
	}
}