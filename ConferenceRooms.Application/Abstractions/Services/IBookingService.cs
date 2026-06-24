using ConferenceRooms.Application.DTO.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.Abstractions.Services
{
	public interface IBookingService
	{
		Task<decimal> AddAsync(AddBookingRequest request);
	}
}
