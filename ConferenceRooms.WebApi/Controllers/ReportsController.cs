using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.DTO.Halls;
using ConferenceRooms.Application.DTO.Reports;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRooms.WebApi.Controllers
{
	[ApiController]
	[Route("api/reports")]
	public class ReportsController : ControllerBase
	{
		private readonly IReportService _reportService;

		public ReportsController(IReportService reportService)
		{
			_reportService = reportService;
		}

		/// <summary>
		/// Get statistic about hall and service popularity
		/// </summary>
		/// <returns>List of halls with statistic, sorted by populatiry</returns>
		[HttpGet("halls-popularity")]
		[ProducesResponseType(typeof(IEnumerable<HallStatisticItem>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetHallsPopularity([FromQuery] ReportFilterRequest filter)
		{
			var statistics = await _reportService.GetHallsPopularityReportAsync(filter);

			return Ok(statistics);
		}
	}
}
