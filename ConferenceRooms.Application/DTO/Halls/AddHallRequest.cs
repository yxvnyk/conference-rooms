using ConferenceRooms.Application.Validators;
using ConferenceRooms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.DTO.Halls
{
	public record AddHallRequest(
		string Name,
		int Capacity,
		IReadOnlyCollection<HallServiceItem>? Services,
		decimal Cost
		);
	
}
