using ConferenceRooms.Application.Abstractions.Repositories;
using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.DTO.Reports;

namespace ConferenceRooms.Application.Services
{
	public class ReportService : IReportService
	{
		private readonly IReportRepository _reportRepository;

		public ReportService(IReportRepository reportRepository)
		{
			_reportRepository = reportRepository;
		}

		public async Task<IEnumerable<HallStatisticItem>> GetHallsPopularityReportAsync(ReportFilterRequest filter)
		{
			return await _reportRepository.GetHallsPopularityReportAsync(filter);
		}
	}
}
