using ConferenceRooms.Application.Validators;

namespace ConferenceRooms.Application.DTO.Halls
{
	public record UpdateHallRequest(
		int? Capacity,
		decimal? Cost,
		IReadOnlyCollection<HallServiceItem> ServiceItems
		);
	
}
