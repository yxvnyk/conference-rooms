using ConferenceRooms.Domain.Entities;

namespace ConferenceRooms.Application.Abstractions.Repositories
{
	public interface IBookingRepository
	{
		Task AddAsync(Booking booking);

		Task<bool> IsHallAvailableAsync(Guid HallId, DateTime StartTime, DateTime EndTime);
	}
}
