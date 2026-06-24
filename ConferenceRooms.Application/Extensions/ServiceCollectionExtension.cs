using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.Interfaces;
using ConferenceRooms.Application.Services;
using ConferenceRooms.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConferenceRooms.Application.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddDtoValidators(this IServiceCollection services) {

			services.AddFluentValidationAutoValidation();
			services.AddFluentValidationClientsideAdapters();
			services.AddValidatorsFromAssemblyContaining<AddBookingRequestValidator>();

			return services;
		}
		public static IServiceCollection AddApplicationServices(this IServiceCollection services) {

			TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
			services.AddScoped<IHallService, HallService>();
			services.AddScoped<IBookingService, BookingService>();

			return services;
		}
	}
}
