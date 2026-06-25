using ConferenceRooms.Application.Abstractions.Repositories;
using ConferenceRooms.Application.DTO.Reports;
using ConferenceRooms.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRooms.Infrastructure.Repositories
{
	public class ReportRepository : IReportRepository
	{
		private readonly IConferenceBookingDbContext _context;

		public ReportRepository(IConferenceBookingDbContext conferenceBookingDbContext)
		{
			this._context = conferenceBookingDbContext;
		}

		public async Task<IEnumerable<HallStatisticItem>> GetHallsPopularityReportAsync(ReportFilterRequest filter)
		{
			// Db is perform this query
			var rawData = await _context.Halls
				.AsNoTracking()
				.Select(hall => new
				{
					Hall = hall,
					FilteredBookings = hall.Bookings.Where(b =>
						(!filter.StartDate.HasValue || b.StartTime >= filter.StartDate.Value) &&
						(!filter.EndDate.HasValue || b.StartTime <= filter.EndDate.Value))
				})
				.Select(x => new
				{
					HallId = x.Hall.Id,
					HallName = x.Hall.Name,
					TotalBookings = x.FilteredBookings.Count(),

					UsedServiceNames = x.FilteredBookings
						.SelectMany(b => b.BookingServices)
						.Select(bs => bs.Service.Name)
				})
				.OrderByDescending(h => h.TotalBookings)
				.ToListAsync(); 

			var report = rawData
				.Select(x => new HallStatisticItem(
					x.HallId,
					x.HallName,
					x.TotalBookings,

					x.UsedServiceNames
						.GroupBy(name => name)
						.Select(group => new ServiceStatisticItem(
							group.Key,
							group.Count()
						))
						.OrderByDescending(s => s.UsageCount)
						.ToList()
				))
				.ToList();

			return report;
		}		
	}
}