using ConferenceRooms.Application.Abstractions.Repositories;
using ConferenceRooms.Application.Interfaces;
using ConferenceRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConferenceRooms.Infrastructure.Repositories
{
	public class ServiceRepository : IServiceRepository
	{
		private readonly IConferenceBookingDbContext _context;

		public ServiceRepository(IConferenceBookingDbContext conferenceBookingDbContext)
		{
			this._context = conferenceBookingDbContext;
		}

		public async Task<bool> AreAllServicesExistAsync(IEnumerable<Guid> serviceIds)
		{
			var existingCount = await _context.Services
				.Where(s => serviceIds.Contains(s.Id))
				.CountAsync();

			return existingCount == serviceIds.Distinct().Count();
		}
	}
}
