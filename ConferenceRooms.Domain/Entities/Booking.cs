namespace ConferenceRooms.Domain.Entities
{
	public class Booking
	{
		public Guid Id { get; set; }

		public Guid HallId { get; set; }

		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		public decimal TotalPrice { get; set; }

		public Hall Hall { get; set; } = null!;

		public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();

	}
}
