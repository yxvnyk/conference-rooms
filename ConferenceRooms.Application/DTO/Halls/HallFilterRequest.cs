using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.DTO.Halls
{
	public record HallFilterRequest(DateTime StartTime, DateTime EndTime, int Capacity);
}
