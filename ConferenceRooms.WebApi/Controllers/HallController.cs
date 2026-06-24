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
			var hallId = await hallService.AddAsync(request);

			return Ok(hallId);
		}

		[HttpDelete]
		public async Task<ActionResult> Remove(Guid guid)
		{
			await hallService.RemoveAsync(guid);
			return NoContent();
		}
	}
}
