using ConferenceRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRooms.Infrastructure.Configurations
{
	public class HallConfiguration : IEntityTypeConfiguration<Hall>
	{
		public void Configure(EntityTypeBuilder<Hall> builder)
		{

			builder.ToTable("halls", t =>
			{
				t.HasCheckConstraint("CK_Halls_Capacity", "\"capacity\" >= 0");
				t.HasCheckConstraint("CK_Halls_Cost", "\"cost\" >= 0");
			});

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasColumnName("id")
				.IsRequired();

			builder.Property(x => x.Name)
				.HasColumnName("name")
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(x => x.Capacity)
				.HasColumnName("capacity")
				.IsRequired();

			builder.Property(x => x.Cost)
				.HasColumnName("cost")
				.HasColumnType("decimal(10,2)")
				.IsRequired();


			builder.HasIndex(x => x.Name)
				.IsUnique();
		}
	}
}
