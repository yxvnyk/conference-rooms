using ConferenceRooms.Application.Validators;

namespace ConferenceRooms.Application.DTO.Halls
{
	public record AddHallRequest(
		string Name,
		int Capacity,
		IReadOnlyCollection<HallServiceItem>? Services,
		decimal Cost
		);
	
}
