using ConferenceRooms.Application.Abstractions.Repositories;
using ConferenceRooms.Application.Interfaces;
using ConferenceRooms.Domain.Entities;
using ConferenceRooms.Infrastracture.Context;
using ConferenceRooms.Infrastructure.Data;
using ConferenceRooms.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ConferenceRooms.Infrastructure.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("ConferenceBookingDb");

			services.AddDbContext<ConferenceBookingDbContext>(options =>
				options.UseNpgsql(connectionString));

			services.AddScoped<IConferenceBookingDbContext>(provider =>
				provider.GetRequiredService<ConferenceBookingDbContext>());

			return services;
		}

		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) {

			services.AddScoped<IHallRepository, HallRepository>();
			services.AddScoped<IServiceRepository, ServiceRepository>();
			services.AddScoped<IBookingRepository, BookingRepository>();
			services.AddScoped<IHallServiceRepository, HallServiceRepository>();
			services.AddScoped<IReportRepository, ReportRepository>();

			return services;
		}
	}
}
