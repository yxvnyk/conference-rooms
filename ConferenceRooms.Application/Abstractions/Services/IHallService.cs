using ConferenceRooms.Application.DTO.Halls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.Abstractions.Services
{
	public interface IHallService
	{
		Task<Guid> Add(AddHallRequest createHallRequest);
	}
}
