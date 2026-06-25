namespace ConferenceRooms.Application.DTO.Reports
{
	public record HallStatisticItem(
		Guid HallId,
		string HallName,
		int TotalBookings,
		IReadOnlyCollection<ServiceStatisticItem> PopularServices
	);
}
