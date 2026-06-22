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
	public class ServiceConfiguration : IEntityTypeConfiguration<Service>
	{
		public void Configure(EntityTypeBuilder<Service> builder)
		{
			builder.ToTable("services");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasColumnName("id")
				.IsRequired();

			builder.Property(x => x.Name)
				.HasColumnName("name")
				.HasMaxLength(100)
				.IsRequired();


			builder.HasIndex(x => x.Name)
				.IsUnique();
		}
	}
}
