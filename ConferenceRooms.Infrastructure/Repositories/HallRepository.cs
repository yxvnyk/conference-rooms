using ConferenceRooms.Application.Abstractions.Repositories;
using ConferenceRooms.Application.Interfaces;
using ConferenceRooms.Application.Queries;
using ConferenceRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ConferenceRooms.Infrastructure.Repositories
{
	public class HallRepository : IHallRepository
	{
		private readonly IConferenceBookingDbContext _context;

		public HallRepository(IConferenceBookingDbContext conferenceBookingDbContext)
		{
			this._context = conferenceBookingDbContext;
		}

		public async Task AddAsync(Hall hall)
		{
			await this._context.Halls.AddAsync(hall);
			await this._context.SaveChangesAsync();
		}

		public async Task RemoveAsync(Guid id)
		{
			var hall = await this._context.Halls.FindAsync(id);
			if (hall != null)
			{
				hall.IsDeleted = true;
				await this._context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Hall>> GetAsync(HallQuery query)
		{
			var q = _context.Halls.AsQueryable();

			if (query.Capacity > 0)
				q = q.Where(x => x.Capacity >= query.Capacity);

			if (query.StartTime.HasValue && query.EndTime.HasValue)
			{
				var start = query.StartTime.Value;
				var end = query.EndTime.Value;

				// TODO: прибрати костиль, та реалізувати Date і Time як окремі поля у базі
				q = q.Where(x => !x.Bookings.Any(b =>
					start <= b.EndTime &&
					end >= b.StartTime));
			}

			return await q.ToListAsync();
		}
		
		public Task<decimal> GetCostAsync(Guid id)
		{
			return _context.Halls
				.AsNoTracking()
				.Where(x => x.Id == id)
				.Select(x => x.Cost)
				.FirstOrDefaultAsync(); ;
		}

		public Task UpdateAsync(Hall hall)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> ExistsByNameAsync(string name)
		{
			return await _context.Halls.AnyAsync(x => x.Name == name);
		}

		public async Task<bool> ExistsByIdAsync(Guid id)
		{
			return await _context.Halls.AnyAsync(x => x.Id == id);
		}

		public Task<Hall>? GetAsync()
		{
			throw new NotImplementedException();
		}
	}
}
