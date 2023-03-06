using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuth.Phone.Entities.Models;

namespace OAuth.Phone.DataAccess.EF.Postgres.Configuration;

internal sealed class UserAuthenticationConfiguration : AuditableEntityConfiguration<UserAuthentication>
{
	public override void Configure(EntityTypeBuilder<UserAuthentication> builder)
	{
		builder.HasIndex(x => x.AuthenticationCode)
			.IsUnique();
		
		builder.Property(x => x.AuthenticationCodeExpiration)
			.HasConversion(x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?),
				x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?));
		
		builder.Property(x => x.AccessTokenExpiration)
			.HasConversion(x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?),
				x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?));
		
		builder.Property(x => x.RefreshTokenExpiration)
			.HasConversion(x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?),
				x => x.HasValue ? x.Value.ToUniversalTime() : default(DateTimeOffset?));
	}
}