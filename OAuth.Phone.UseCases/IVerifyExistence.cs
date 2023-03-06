namespace OAuth.Phone.UseCases;

public interface IVerifyExistence<in TRequest>
{
	Task<bool> IsExistsAsync(TRequest request, CancellationToken cancellationToken);
}