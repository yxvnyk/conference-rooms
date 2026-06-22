using ConferenceRooms.Application.Interfaces;
using ConferenceRooms.Infrastracture.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Infrastructure.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration) {

			var connectionString = configuration.GetConnectionString("ConferenceBookingDb");
			services.AddDbContext<IConferenceBookingDbContext, ConferenceBookingDbContext>(options =>
				options.UseNpgsql(connectionString)); 

			return services;
		}
	}
}
