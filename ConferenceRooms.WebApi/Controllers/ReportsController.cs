using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.DTO.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRooms.WebApi.Controllers
{
	/// <summary>
	/// Controller for generating business reports and analytics.
	/// </summary>
	[ApiController]
	[Route("api/reports")]
	public class ReportsController : ControllerBase
	{
		private readonly IReportService _reportService;

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportsController"/> class.
		/// </summary>
		/// <param name="reportService">The service responsible for report generation business logic.</param>
		public ReportsController(IReportService reportService)
		{
			_reportService = reportService;
		}

		/// <summary>
		/// Retrieves statistics about hall and service popularity.
		/// </summary>
		/// <remarks>
		/// You can optionally filter the statistics by a specific date range.
		/// 
		/// Sample request:
		/// 
		///     GET /api/reports/halls-popularity?startDate=2024-09-01&amp;endDate=2024-09-30
		///     
		/// </remarks>
		/// <param name="filter">Optional parameters to filter the report by date range.</param>
		/// <returns>A list of halls with their statistics, sorted by popularity.</returns>
		/// <response code="200">Successfully retrieved the statistics report.</response>
		/// <response code="400">If the provided filter dates are invalid.</response>
		[HttpGet("halls-popularity")]
		[ProducesResponseType(typeof(IEnumerable<HallStatisticItem>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetHallsPopularity([FromQuery] ReportFilterRequest filter)
		{
			var statistics = await _reportService.GetHallsPopularityReportAsync(filter);

			return Ok(statistics);
		}
	}
}