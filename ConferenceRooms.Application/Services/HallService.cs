using ConferenceRooms.Application.Abstractions;
using ConferenceRooms.Application.Abstractions.Repositories;
using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.DTO.Halls;
using ConferenceRooms.Application.Queries;
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

		public async Task<IEnumerable<Hall>> GetAsync(HallFilterRequest request)
		{
			var query = request.Adapt<HallQuery>();

			var list = await _hallRepository.GetAsync(query);

			return list;
		}

		public async Task<bool> UpdateAsync(Guid id, UpdateHallRequest request)
		{
			var hall = await _hallRepository.GetByIdWithHallServicesAsync(id);

			if (hall is null)
				return false;

			UpdateBaseProperties(hall, request);
			SyncServices(hall, request.ServiceItems);

			await _hallRepository.SaveChangesAsync();

			return true;
		}

		private static void UpdateBaseProperties(Hall hall, UpdateHallRequest request)
		{
			if (request.Capacity != null)
			{
				hall.Capacity = (int)request.Capacity;
			}

			if (request.Cost != null)
			{
				hall.Cost = (decimal)request.Cost;
			}
		}

		private static void SyncServices(Hall hall, IReadOnlyCollection<HallServiceItem> incomingServices)
		{
			var incomingIds = incomingServices.Select(s => s.ServiceId).ToHashSet();

			var servicesToRemove = hall.HallServices
				.Where(existingService => !incomingIds.Contains(existingService.ServiceId))
				.ToList(); 

			foreach (var service in servicesToRemove)
			{
				hall.HallServices.Remove(service);
			}

			foreach (var incomingItem in incomingServices)
			{
				var existingService = hall.HallServices.FirstOrDefault(s => s.ServiceId == incomingItem.ServiceId);

				if (existingService is not null)
				{
					existingService.Price = incomingItem.Price;
				}
				else
				{
					hall.HallServices.Add(new Domain.Entities.HallService
					{
						HallId = hall.Id,
						ServiceId = incomingItem.ServiceId,
						Price = incomingItem.Price
					});
				}
			}
		}
		public async Task<Guid> AddAsync(AddHallRequest request)
		{
			await VerifyHallName(request.Name);

			await VerifyServiceIds(request.Services);

			var hall = request.Adapt<Hall>();

			await _hallRepository.AddAsync(hall);

			return hall.Id;
		}

		public async Task RemoveAsync(Guid id)
		{
			await _hallRepository.RemoveAsync(id);
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
