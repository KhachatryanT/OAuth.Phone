using JetBrains.Annotations;
using MediatR;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;
using OAuth.Phone.Infrastructure.Interfaces.Services;
using OAuth.Phone.Utils;

namespace OAuth.Phone.UseCases.Handlers.Commands.SignIn;

[UsedImplicitly]
internal sealed class SignInCommandHandler : ICommandHandler<SignInCommand>
{
	private readonly IDbContext _dbContext;
	private readonly IAuthenticateService _authenticateService;

	public SignInCommandHandler(IDbContext dbContext, IAuthenticateService authenticateService)
	{
		_dbContext = dbContext;
		_authenticateService = authenticateService;
	}

	public async Task<Unit> Handle(SignInCommand request, CancellationToken cancellationToken)
	{
		var user = _dbContext.Users.Single(x => x.Phone == request.Phone);

		if (user.ConfirmationCode != request.Code)
		{
			user.ConfirmationErrorsCount++;
			await _dbContext.SaveChangesAsync(cancellationToken);
			throw new BadRequestException(ErrorCodes.ConfirmationCodeIncorrect);
		}

		await _authenticateService.SignInAsync(user, Defaults.AuthenticationScheme);
		return Unit.Value;
	}
}