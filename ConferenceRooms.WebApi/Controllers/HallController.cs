using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.DTO.Halls;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRooms.WebApi.Controllers
{
	/// <summary>
	/// Controller for managing conference halls, including creation, updates, deletion, and retrieval.
	/// </summary>
	[ApiController]
	[Route("api/halls")]
	public class HallController : ControllerBase
	{
		private readonly IHallService hallService;

		/// <summary>
		/// Initializes a new instance of the <see cref="HallController"/> class.
		/// </summary>
		/// <param name="hallService">The service responsible for hall business logic.</param>
		public HallController(IHallService hallService)
		{
			this.hallService = hallService;
		}

		/// <summary>
		/// Retrieves a list of halls based on the provided filter criteria (e.g., availability, capacity).
		/// </summary>
		/// <param name="request">The filtering parameters such as time range and capacity.</param>
		/// <returns>A collection of halls matching the criteria.</returns>
		/// <response code="200">Successfully retrieved the list of halls.</response>
		/// <response code="400">If the filter parameters are invalid.</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Get([FromQuery] HallFilterRequest request)
		{
			var halls = await hallService.GetAsync(request);

			return Ok(halls);
		}

		/// <summary>
		/// Creates a new conference hall with its base details and available services.
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// 
		///     POST /api/halls
		///     {
		///        "name": "Hall A",
		///        "capacity": 50,
		///        "cost": 2000,
		///        "serviceItems": [
		///           { "serviceId": "1fa85f64-5717-4562-b3fc-2c963f66afa1", "price": 500 }
		///        ]
		///     }
		///     
		/// </remarks>
		/// <param name="request">The details of the new hall to create.</param>
		/// <returns>The unique identifier of the newly created hall.</returns>
		/// <response code="200">Successfully created the hall.</response>
		/// <response code="400">If the request data is invalid (e.g., negative capacity).</response>
		/// <response code="409">If a hall with the same name already exists.</response>
		[HttpPost]
		[ProducesResponseType(typeof(AddHallResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		public async Task<ActionResult> Add(AddHallRequest request)
		{
			var hallId = await hallService.AddAsync(request);

			var response = new AddHallResponse(hallId);

			return Ok(response);
		}

		/// <summary>
		/// Updates the information and services of an existing conference hall.
		/// </summary>
		/// <param name="id">The unique identifier of the hall to update.</param>
		/// <param name="request">The updated hall details.</param>
		/// <returns>No content if the update is successful.</returns>
		/// <response code="204">Successfully updated the hall.</response>
		/// <response code="400">If the request data is invalid.</response>
		/// <response code="404">If the hall with the specified ID was not found.</response>
		[HttpPut("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] UpdateHallRequest request)
		{
			var isUpdated = await hallService.UpdateAsync(id, request);

			if (!isUpdated)
			{
				return NotFound(new { Message = $"Hall with ID {id} not found." });
			}

			return NoContent();
		}

		/// <summary>
		/// Deletes a specific conference hall from the system.
		/// </summary>
		/// <param name="id">The unique identifier of the hall to delete.</param>
		/// <returns>No content if the deletion is successful.</returns>
		/// <response code="204">Successfully deleted the hall.</response>
		/// <response code="404">If the hall with the specified ID was not found.</response>
		[HttpDelete("{id:guid}")] 
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Remove([FromRoute] Guid id)
		{
			await hallService.RemoveAsync(id);
			return NoContent();
		}
	}
}