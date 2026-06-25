using ConferenceRooms.Application.DTO.Halls;
using ConferenceRooms.Application.DTO.Reports;
using ConferenceRooms.Domain.Entities;

namespace ConferenceRooms.Application.Abstractions.Services
{
	public interface IReportService
	{
		Task<IEnumerable<HallStatisticItem>> GetHallsPopularityReportAsync(ReportFilterRequest filter);
	}
}
