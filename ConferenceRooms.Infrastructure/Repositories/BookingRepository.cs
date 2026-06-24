using ConferenceRooms.Application.Abstractions.Repositories;
using ConferenceRooms.Application.Interfaces;
using ConferenceRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRooms.Infrastructure.Repositories
{
	public class BookingRepository : IBookingRepository
	{
		private readonly IConferenceBookingDbContext _context;

		public BookingRepository(IConferenceBookingDbContext conferenceBookingDbContext)
		{
			this._context = conferenceBookingDbContext;
		}

		public async Task AddAsync(Booking booking)
		{
			await this._context.Bookings.AddAsync(booking);
			await this._context.SaveChangesAsync();
		}

		public async Task<bool> IsHallAvailableAsync(Guid hallId, DateTime startTime, DateTime endTime)
		{
			bool hasConflict = await this._context.Bookings.AnyAsync(x =>
				x.HallId == hallId &&
				(
					(startTime >= x.StartTime && startTime < x.EndTime) ||
					(endTime > x.StartTime && endTime <= x.EndTime) ||
					(startTime <= x.StartTime && endTime >= x.EndTime)
				));

			return !hasConflict;
		}
	}
}
