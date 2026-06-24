namespace ConferenceRooms.Domain.Entities
{
	public class Hall
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;

		public int Capacity { get; set; }

		public decimal Cost { get; set; }

		public bool IsDeleted { get; set; } = false;

		public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

		public ICollection<HallService> HallServices { get; set; } = new List<HallService>();
	}
}
