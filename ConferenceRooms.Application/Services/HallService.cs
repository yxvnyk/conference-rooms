using ConferenceRooms.Application.Abstractions;
using ConferenceRooms.Application.Abstractions.Repositories;
using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.DTO.Halls;
using ConferenceRooms.Application.Validators;
using ConferenceRooms.Domain.Entities;
using ConferenceRooms.Domain.Exceptions;
using Mapster;

namespace ConferenceRooms.Application.Services
{
	public class HallService : IHallService
	{
		private readonly IHallRepository _hallRepository;
		private readonly IServiceRepository _serviceRepository;
		public HallService(IHallRepository hallRepository, IServiceRepository serviceRepository) {
			_hallRepository = hallRepository;
			_serviceRepository = serviceRepository;
		}

		public async Task<Guid> Add(AddHallRequest request)
		{
			await VerifyHallName(request.Name);

			await VerifyServiceIds(request.Services);

			var hall = request.Adapt<Hall>();


			await _hallRepository.CreateAsync(hall);

			return hall.Id;
		}

		private async Task VerifyServiceIds(IReadOnlyCollection<HallServiceItem>? hallServiceItems)
		{
			if (hallServiceItems == null || !hallServiceItems.Any())
			{
				return;
			}

			var ids = hallServiceItems.Select(x => x.ServiceId);

			if (!await _serviceRepository.AreAllServicesExistAsync(ids))
			{
				throw new ServiceNotExistException();
			}
		}

		private async Task VerifyHallName(string name)
		{
			if (await _hallRepository.ExistsByNameAsync(name))
			{
				throw new NotUniqueHallNameException();
			}
		}
	}
}
