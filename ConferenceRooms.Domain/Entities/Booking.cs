namespace ConferenceRooms.Domain.Entities
{
	public class Booking
	{
		public Guid Id { get; set; }

		public Guid HallId { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public decimal TotalPrice { get; set; }

		public Hall Hall { get; set; } = null!;

		public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();

	}
}
