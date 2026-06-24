using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.DTO.Booking;
using ConferenceRooms.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRooms.WebApi.Controllers
{
	[ApiController]
	[Route("api/bookings")]
	public class BookingController : Controller
	{
		private readonly IBookingService bookingService;

		public BookingController(IBookingService bookingService) {
			this.bookingService = bookingService; 
		}

		[HttpPost]
		public async Task<ActionResult> Add(AddBookingRequest request)
		{
			var cost = await bookingService.AddAsync(request);

			var repsonse = new AddBookingResponse(cost);

			return Ok(repsonse);
		}
	}
}
