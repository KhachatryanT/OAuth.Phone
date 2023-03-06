using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OAuth.Phone.Entities.Models;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;
using OAuth.Phone.Infrastructure.Interfaces.Services;
using OAuth.Phone.UseCases.Handlers.Commands.CreateUser;
using OAuth.Phone.UseCases.Utils;
using OAuth.Phone.Utils;
using OAuth.Phone.Utils.Settings;

namespace OAuth.Phone.UseCases.Handlers.Commands.SendConfirmationCode;

[UsedImplicitly]
internal sealed class SendConfirmationCodeCommandHandler : ICommandHandler<SendConfirmationCodeCommand>
{
	private readonly IDbContext _dbContext;
	private readonly ISender _sender;
	private readonly IOptions<ConfirmationCodeSettings> _confirmationCodeSettings;
	private readonly INotificationService _notificationService;

	public SendConfirmationCodeCommandHandler(IDbContext dbContext,
		ISender sender,
		IOptions<ConfirmationCodeSettings> confirmationCodeSettings,
		INotificationService notificationService)
	{
		_dbContext = dbContext;
		_sender = sender;
		_confirmationCodeSettings = confirmationCodeSettings;
		_notificationService = notificationService;
	}

	public async Task<Unit> Handle(SendConfirmationCodeCommand request, CancellationToken cancellationToken)
	{
		var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Phone == request.Phone, cancellationToken)
		           ?? (await _sender.Send(new CreateUserCommand(request.Phone), cancellationToken)).User;

		ObtainNewConfirmationCode(user);
		var transaction = _dbContext.BeginTransaction();

		try
		{
			await _notificationService.NotifyAsync(user.Phone, user.ConfirmationCode!.Value, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);
			await transaction.CommitAsync(cancellationToken);
		}
		catch (Exception)
		{
			await transaction.RollbackAsync(cancellationToken);
			throw;
		}

		return Unit.Value;
	}

	private void ObtainNewConfirmationCode(User user)
	{
		user.ConfirmationCode = RandomUtils.NextConfirmationCode();
		user.ConfirmationCodeAvailableUntil =
			DateTimeOffset.Now.Add(_confirmationCodeSettings.Value.Expiration);
		user.ConfirmationErrorsCount = 0;
		user.NextRequestConfirmationCodeAvailableAt =
			DateTimeOffset.Now.Add(_confirmationCodeSettings.Value.RequestConfirmationCodeInterval);
	}
}