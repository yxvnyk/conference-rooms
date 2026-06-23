using ConferenceRooms.Application.Abstractions.Services;
using ConferenceRooms.Application.Interfaces;
using ConferenceRooms.Application.Services;
using ConferenceRooms.Application.Validators;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConferenceRooms.Application.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddDtoValidators(this IServiceCollection services) {

			// Automatically register all validators in the same assembly
			services.AddValidatorsFromAssemblyContaining<HallServiceItemValidator>();

			return services;
		}
		public static IServiceCollection AddApplicationServices(this IServiceCollection services) {

			TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
			services.AddScoped<IHallService, HallService>();

			return services;
		}
	}
}
