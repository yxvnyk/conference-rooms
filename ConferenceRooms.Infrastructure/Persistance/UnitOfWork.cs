using ConferenceRooms.Application.Abstractions;
using ConferenceRooms.Infrastracture.Context;

namespace ConferenceRooms.Infrastructure.Persistance
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ConferenceBookingDbContext _context;
		public UnitOfWork(ConferenceBookingDbContext context) => _context = context;
		public Task<int> SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);
	}
}
