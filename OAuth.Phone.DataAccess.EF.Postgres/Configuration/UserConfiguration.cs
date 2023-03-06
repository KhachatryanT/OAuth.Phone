using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuth.Phone.Entities.Models;

namespace OAuth.Phone.DataAccess.EF.Postgres.Configuration;

internal sealed class UserConfiguration : AuditableEntityConfiguration<User>
{
	public override void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasIndex(x => x.Phone)
			.IsUnique();
		
		builder.Property(x => x.LastSignIn)
			.HasConversion(x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?),
				x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?));

		builder.Property(x => x.ConfirmationCodeAvailableUntil)
			.HasConversion(x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?),
				x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?));

		builder.Property(x => x.NextRequestConfirmationCodeAvailableAt)
			.HasConversion(x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?),
				x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?));
	}
}