using ConferenceRooms.Application.Abstractions.Repositories;
using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.DTO.Booking;
using ConferenceRooms.Application.Helpers;
using ConferenceRooms.Domain.Entities;
using ConferenceRooms.Domain.Exceptions;
using Mapster;

namespace ConferenceRooms.Application.Services
{
	public class BookingService : IBookingService
	{
	    private readonly IBookingRepository _bookingRepository;
	    private readonly IServiceRepository _serviceRepository;
	    private readonly IHallRepository _hallRepository;
	    private readonly IHallServiceRepository _hallServiceRepository; 
	    
	    public BookingService(
	        IBookingRepository bookingRepository, 
	        IServiceRepository serviceRepository, 
	        IHallRepository hallRepository,
	        IHallServiceRepository hallServiceRepository) 
	    {
	        _bookingRepository = bookingRepository;
	        _serviceRepository = serviceRepository;
	        _hallRepository = hallRepository;
	        _hallServiceRepository = hallServiceRepository;
	    }

		public async Task<decimal> AddAsync(AddBookingRequest request)
		{
			await VerifyHallExist(request.HallId);

			await VerifyServiceIds(request.ServiceIds);

			var boking = request.Adapt<Booking>();

			await VerifyHallAvailability(boking);
		
			var hallServices = await _hallServiceRepository.GetHallServicesAsync(boking.HallId, request.ServiceIds);
			var serviceIds = hallServices.Select(x => x.ServiceId);
			var servicePrices = hallServices.Select(x => x.Price);

			AssignBookingServicePricesAndValidate(boking, hallServices);

			decimal cost = await CalculateTotalPriceAsync(boking, servicePrices);

			boking.TotalPrice = cost;

			await _bookingRepository.AddAsync(boking);

			return cost;
		}

		private async Task<decimal> CalculateTotalPriceAsync(Booking booking, IEnumerable<decimal> servicePrices)
		{
			var hallBaseCost = await _hallRepository.GetCostAsync(booking.HallId);

			if (hallBaseCost == 0) throw new HallNotExistException();

			var totalPrice = BookingPriceHelper.CalculateTotalPrice(
				hallBaseCost,
				servicePrices,
				booking.StartTime,
				booking.EndTime);

			return totalPrice;
		}

		private void AssignBookingServicePricesAndValidate(Booking booking, List<Domain.Entities.HallService>? hallServices)
		{
			if (hallServices == null) return;

			foreach (var item in booking.BookingServices)
			{
				var hs = hallServices.FirstOrDefault(x => x.ServiceId == item.ServiceId);
				if (hs != null)
				{
					item.LockedPrice = hs.Price;
				}
			}
		}

		private async Task VerifyServiceIds(IReadOnlyCollection<Guid>? ids)
		{
			if (ids == null)
			{
				return;
			}

			if (!await _serviceRepository.AreAllServicesExistAsync(ids))
			{
				throw new ServiceNotExistException();
			}
		}

		private async Task VerifyHallExist(Guid id)
		{
			if (!await _hallRepository.ExistsByIdAsync(id))
			{
				throw new HallNotExistException();
			}
		}

		private async Task VerifyHallAvailability(Booking booking)
		{
			if (!await _bookingRepository.IsHallAvailableAsync(booking.HallId, booking.StartTime, booking.EndTime))
			{
				throw new HallIsNotAvailableException();
			}
		}
	}
}
