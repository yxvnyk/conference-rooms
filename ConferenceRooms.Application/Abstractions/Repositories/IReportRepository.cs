using ConferenceRooms.Application.DTO.Reports;

namespace ConferenceRooms.Application.Abstractions.Repositories
{
	public interface IReportRepository
	{
		Task<IEnumerable<HallStatisticItem>> GetHallsPopularityReportAsync(ReportFilterRequest filter);

	}
}
