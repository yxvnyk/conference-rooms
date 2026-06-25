namespace ConferenceRooms.Application.DTO.Reports
{
	public record ReportFilterRequest(
		DateTime? StartDate,
		DateTime? EndDate
	);
}
