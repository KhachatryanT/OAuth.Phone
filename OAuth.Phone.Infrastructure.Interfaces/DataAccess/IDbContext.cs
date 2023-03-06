using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OAuth.Phone.Entities.Models;

namespace OAuth.Phone.Infrastructure.Interfaces.DataAccess;

public interface IDbContext
{
	public DbSet<User> Users { get; }
	public DbSet<UserAuthentication> UserAuthentications { get; }

	IDbContextTransaction BeginTransaction();
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}