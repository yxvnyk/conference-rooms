using ConferenceRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRooms.Infrastructure.Configurations
{
	public class BookingServiceConfiguration : IEntityTypeConfiguration<BookingService>
	{
		public void Configure(EntityTypeBuilder<BookingService> builder)
		{
			builder.ToTable("booking_services", t =>
			{
				t.HasCheckConstraint(
					"CK_Booking_Services_PriceAtBooking",
					"price_at_booking >= 0");
			});

			builder.HasKey(x => new { x.BookingId, x.ServiceId });

			builder.Property(x => x.BookingId)
				.HasColumnName("booking_id")
				.IsRequired();

			builder.Property(x => x.ServiceId)
				.HasColumnName("service_id")
				.IsRequired();

			builder.Property(x => x.Price)
				.HasColumnName("price_at_booking")
				.HasColumnType("decimal(10,2)")
				.IsRequired();

			builder.HasOne(x => x.Service)
				.WithMany(s => s.BookingServices)
				.HasForeignKey(x => x.ServiceId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(x => new { x.BookingId, x.ServiceId });

		}
	}
}
