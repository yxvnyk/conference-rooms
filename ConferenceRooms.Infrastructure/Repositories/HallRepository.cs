using ConferenceRooms.Application.Abstractions.Repositories;
using ConferenceRooms.Application.Interfaces;
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

		public async Task CreateAsync(Hall hall)
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

		public Task<Hall>? GetAsync()
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Hall hall)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> ExistsByNameAsync(string name)
		{
			return await _context.Halls.AnyAsync(x => x.Name == name);
		}
	}
}
