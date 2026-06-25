namespace ConferenceRooms.Application.DTO.Halls
{
	public record HallFilterRequest(DateTime StartTime, int Duration, int Capacity);
}
