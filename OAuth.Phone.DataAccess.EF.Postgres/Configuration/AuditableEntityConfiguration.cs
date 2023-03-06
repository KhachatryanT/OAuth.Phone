using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth.Phone.DataAccess.EF.Postgres.Configuration;

internal abstract class AuditableEntityConfiguration<T> : RowVersionEntityConfiguration<T> where T : class
{
	public override void Configure(EntityTypeBuilder<T> builder)
	{
		base.Configure(builder);

		builder.Property<DateTimeOffset>("Created")
			.HasDefaultValueSql("now()")
			.ValueGeneratedOnAdd();

		// todo не работает обновление даты
		builder.Property<DateTimeOffset>("Modified")
			.ValueGeneratedOnUpdate()
			.HasDefaultValueSql("now()")
			.Metadata
			.SetAfterSaveBehavior(PropertySaveBehavior.Save);
	}
}