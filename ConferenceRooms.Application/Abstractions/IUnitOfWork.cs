namespace ConferenceRooms.Application.Abstractions
{
	public interface IUnitOfWork
	{
		Task<int> SaveChangesAsync(CancellationToken ct = default);
	}
}
