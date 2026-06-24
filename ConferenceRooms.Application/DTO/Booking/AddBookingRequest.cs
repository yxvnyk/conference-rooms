namespace ConferenceRooms.Application.DTO.Booking
{
	public record AddBookingRequest(
		Guid HallId,
		DateTime StartTime,
		int DurationHours,
		IReadOnlyCollection<Guid> ServiceIds
		);
	
}
