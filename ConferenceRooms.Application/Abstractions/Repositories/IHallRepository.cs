using ConferenceRooms.Application.Queries;
using ConferenceRooms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.Abstractions.Repositories
{
	public interface IHallRepository
	{
		Task<Hall>? GetAsync();

		Task AddAsync(Hall hall);

		Task UpdateAsync(Hall hall);

		Task RemoveAsync(Guid id);

		Task<bool> ExistsByNameAsync(string name);

		Task<bool> ExistsByIdAsync(Guid id);

		Task<decimal> GetCostAsync(Guid id);

		Task<IEnumerable<Hall>> GetAsync(HallQuery query);

	}
}
