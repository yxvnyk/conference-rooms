namespace ConferenceRooms.Domain.Entities
{
	public class HallService
	{
		public Guid HallId { get; set; }
		
		public Guid ServiceId { get; set; }
		
		public decimal Price { get; set; }

		public Hall Hall { get; set; } = null!;

		public Service Service { get; set; } = null!;

	}
}
