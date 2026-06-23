using ConferenceRooms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.Abstractions.Repositories
{
	public interface IServiceRepository
	{
		Task<bool> AreAllServicesExistAsync(IEnumerable<Guid> serviceIds);

	}
}
