using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.DTO.Booking;
using ConferenceRooms.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRooms.WebApi.Controllers
{
	/// <summary>
	/// Controller for managing conference room bookings.
	/// </summary>
	[ApiController]
	[Route("api/bookings")]
	public class BookingController : Controller 
	{
		private readonly IBookingService bookingService;

		/// <summary>
		/// Initializes a new instance of the <see cref="BookingController"/> class.
		/// </summary>
		/// <param name="bookingService">The service responsible for booking business logic.</param>
		public BookingController(IBookingService bookingService)
		{
			this.bookingService = bookingService;
		}

		/// <summary>
		/// Creates a new booking and calculates the total cost of the rent.
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// 
		///     POST /api/bookings
		///     {
		///        "hallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
		///        "bookingDate": "2024-09-01T10:00:00Z",
		///        "durationInHours": 4,
		///        "serviceIds": ["1fa85f64-5717-4562-b3fc-2c963f66afa1"]
		///     }
		///     
		/// </remarks>
		/// <param name="request">The booking details, including the hall ID, time, duration, and selected services.</param>
		/// <returns>A confirmation of the booking containing the calculated total cost.</returns>
		/// <response code="200">Successfully created the booking and returned the total cost.</response>
		/// <response code="400">If the request data is invalid (e.g., negative duration or past date).</response>
		/// <response code="409">If there is a conflict (e.g., the hall is already booked for the selected time or does not exist).</response>
		[HttpPost]
		[ProducesResponseType(typeof(AddBookingResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		public async Task<ActionResult> Add(AddBookingRequest request)
		{
			var cost = await bookingService.AddAsync(request);

			var response = new AddBookingResponse(cost);

			return Ok(response);
		}
	}
}