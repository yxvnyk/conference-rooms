namespace ConferenceRooms.Application.Queries
{
	public class HallQuery
	{
		public DateTime? StartTime { get; set; }

		public DateTime? EndTime { get; set; }
			
		public int Capacity { get; set; }
	}
}
