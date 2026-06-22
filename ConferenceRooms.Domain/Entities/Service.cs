namespace ConferenceRooms.Domain.Entities
{
	public class Service
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = string.Empty;


		public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();

		public ICollection<HallService> HallServices { get; set; } = new List<HallService>();
	}
}
