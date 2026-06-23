using ConferenceRooms.Application.DTO.Halls;
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
	public class HallMappingConfig : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<AddHallRequest, Hall>()
				.Map(dest => dest.Id, src => Guid.NewGuid())
				.Map(dest => dest.Name, src => src.Name)
				.Map(dest => dest.Capacity, src => src.Capacity)
				.Map(dest => dest.Cost, src => src.Cost)
				.Map(dest => dest.HallServices, src => src.Services);

			config.NewConfig<HallServiceItem, HallService>()
				.Map(dest => dest.ServiceId, src => src.ServiceId)
				.Map(dest => dest.Price, src => src.Price)
				.Ignore(dest => dest.HallId);
		}
	}
}
