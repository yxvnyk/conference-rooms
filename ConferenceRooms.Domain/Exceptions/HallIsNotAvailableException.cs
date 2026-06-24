namespace ConferenceRooms.Domain.Exceptions
{
	public class HallIsNotAvailableException(string message = "Hall is not available at that time", string title = "Invalid data") :
		BaseCustomlException(message, 409, title);
}
