using ConferenceRooms.Application.DTO.Booking;
using ConferenceRooms.Application.Validators;
using ConferenceRooms.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.Mappings
{
	public class BookingMappingConfig : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<AddBookingRequest, Booking>()
				.Map(dest => dest.Id, src => Guid.NewGuid())
				.Map(dest => dest.HallId, src => src.HallId)
				.Map(dest => dest.StartTime, src => src.StartTime)
				.Map(dest => dest.EndTime, src => src.StartTime.AddHours(src.DurationHours))
				.Map(dest => dest.BookingServices, src => src.ServiceIds);

			config.NewConfig<Guid, BookingService>()
				.Map(dest => dest.ServiceId, src => src)
				.Ignore(dest => dest.BookingId);
		}
	}
}
