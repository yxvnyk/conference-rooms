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

		Task CreateAsync(Hall hall);

		Task UpdateAsync(Hall hall);

		Task DeleteAsync(Guid id);

		Task<bool> ExistsByNameAsync(string name);

	}
}
