using ConferenceRooms.Application.Abstractions.Repositories;
using ConferenceRooms.Application.Interfaces;
using ConferenceRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRooms.Infrastructure.Repositories
{
	public class HallServiceRepository : IHallServiceRepository
	{
		private readonly IConferenceBookingDbContext _context;

		public HallServiceRepository(IConferenceBookingDbContext conferenceBookingDbContext)
		{
			this._context = conferenceBookingDbContext;
		}
		public Task<List<HallService>> GetHallServicesAsync(Guid hallId, IEnumerable<Guid> serviceIds)
		{
			return this._context.HallServices
				.AsNoTracking()
				.Where(hs => hs.HallId == hallId && serviceIds
					.Contains(hs.ServiceId))
				.ToListAsync();
		}
	}
}
