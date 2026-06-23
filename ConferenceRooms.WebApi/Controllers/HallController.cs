using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.DTO.Halls;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRooms.WebApi.Controllers
{
	[ApiController]
	[Route("api/halls")]
	public class HallController : Controller
	{
		private readonly IHallService hallService;

		public HallController(IHallService hallService) {
			this.hallService = hallService; 
		}

		[HttpPost]
		public async Task<ActionResult> Add(AddHallRequest request)
		{
			var hallId = await hallService.Add(request);

			// Возвращаем 201 Created с путем к новому объекту
			return CreatedAtAction(nameof(Add), new { id = hallId }, new { Id = hallId });
		}
	}
}
