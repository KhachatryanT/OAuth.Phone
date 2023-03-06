using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth.Phone.DataAccess.EF.Postgres.Configuration;

internal abstract class RowVersionEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
	public virtual void Configure(EntityTypeBuilder<T> builder)
	{
		builder.Property<byte[]>("RowVersion")
			.IsRowVersion();
	}
}