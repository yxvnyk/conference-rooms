using ConferenceRooms.Application.DTO.Halls;
using ConferenceRooms.Domain.Entities;

namespace ConferenceRooms.Application.Abstractions.Services
{
	public interface IHallService
	{
		Task<Guid> AddAsync(AddHallRequest createHallRequest);

		Task RemoveAsync(Guid id);

		Task<IEnumerable<Hall>> GetAsync(HallFilterRequest request);
	}
}
