using ConferenceRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRooms.Application.Interfaces
{
	public interface IConferenceBookingDbContext
	{
		DbSet<Hall> Halls {get;} 

		DbSet<Service> Services {get;} 
		
		DbSet<Booking> Bookings {get;}

		DbSet<HallService> HallServices { get; }

		DbSet<BookingService> BookingServices { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
