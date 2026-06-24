using ConferenceRooms.Domain.Entities;

namespace ConferenceRooms.Application.Abstractions.Repositories
{
	public interface IHallServiceRepository
	{
		Task<List<HallService>> GetHallServicesAsync(Guid hallId, IEnumerable<Guid> serviceIds);

	}
}
