using ConferenceRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ConferenceRooms.Infrastructure.Configurations
{
	public class BookingConfiguration : IEntityTypeConfiguration<Booking>
	{
		public void Configure(EntityTypeBuilder<Booking> builder)
		{

			builder.ToTable("bookings", t =>
			{
				t.HasCheckConstraint("CK_Bookings_Time", "end_time > start_time");
				t.HasCheckConstraint("CK_Bookings_TotalPrice", "total_price >= 0");
			});

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasColumnName("id")
				.IsRequired();

			builder.Property(x => x.HallId)
				.HasColumnName("hall_id")
				.IsRequired();

			builder.Property(x => x.StartTime)
				.HasColumnName("start_time")
				.IsRequired();

			builder.Property(x => x.EndTime)
				.HasColumnName("end_time")
				.IsRequired();

			builder.Property(x => x.TotalPrice)
				.HasColumnName("total_price")
				.HasColumnType("decimal(10,2)")
				.IsRequired();

			builder.HasOne(x => x.Hall)
				.WithMany(h => h.Bookings)
				.HasForeignKey(x => x.HallId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(x => x.BookingServices)
			   .WithOne(bs => bs.Booking)
			   .HasForeignKey(bs => bs.BookingId)
			   .OnDelete(DeleteBehavior.Restrict);


			builder.HasIndex(x => x.HallId);
			builder.HasIndex(x => new { x.HallId, x.StartTime, x.EndTime });
		}
	}
}
