using ConferenceRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Infrastructure.Configurations
{
	public class HallServiceConfiguration : IEntityTypeConfiguration<HallService>
	{
		public void Configure(EntityTypeBuilder<HallService> builder)
		{
			builder.ToTable("hall_services", t =>
			{
				t.HasCheckConstraint("CK_Hall_Service_Price", "\"price\" >= 0");
			});


			builder.HasKey(x => new {x.HallId, x.ServiceId});

			builder.Property(x => x.HallId)
				.HasColumnName("hall_id")
				.IsRequired();

			builder.Property(x => x.ServiceId)
				.HasColumnName("service_id")
				.IsRequired();

			builder.Property(x => x.Price)
				.HasColumnName("price")
				.HasColumnType("decimal(10,2)")
				.IsRequired();

			builder.HasOne(x => x.Hall)
				.WithMany(h => h.HallServices)
				.HasForeignKey(x => x.HallId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(x => x.Service)
				.WithMany(s => s.HallServices)
				.HasForeignKey(x => x.ServiceId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(x => x.HallId);
			builder.HasIndex(x => x.ServiceId);
		}
	}
}
