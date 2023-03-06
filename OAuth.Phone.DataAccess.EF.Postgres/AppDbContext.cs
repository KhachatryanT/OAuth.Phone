using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OAuth.Phone.Entities.Models;
using OAuth.Phone.Infrastructure.Interfaces.DataAccess;

namespace OAuth.Phone.DataAccess.EF.Postgres;

public sealed class AppDbContext : DbContext, IDbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users { get; init; } = default!;
	public DbSet<UserAuthentication> UserAuthentications { get; init; } = default!;

	public IDbContextTransaction BeginTransaction() => Database.BeginTransaction();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}
}