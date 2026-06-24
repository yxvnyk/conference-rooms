using ConferenceRooms.Domain.Entities;
using ConferenceRooms.Infrastracture.Context;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRooms.Infrastructure.Data
{
	public static class DataSeeder
	{
		private static readonly Guid HallAId = new Guid("11111111-1111-1111-1111-111111111111");
		private static readonly Guid HallBId = new Guid("22222222-2222-2222-2222-222222222222");
		private static readonly Guid HallCId = new Guid("33333333-3333-3333-3333-333333333333");

		private static readonly Guid ProjId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
		private static readonly Guid WifiId = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
		private static readonly Guid SoundId = new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc");
		public static async Task SeedAsync(ConferenceBookingDbContext context)
		{
			if (await context.Halls.AnyAsync()) return; 

			var halls = GetHalls();
			var services = GetServices();
			var hallServices = GetHallServices(halls, services);

			await context.Halls.AddRangeAsync(halls);
			await context.Services.AddRangeAsync(services);
			await context.HallServices.AddRangeAsync(hallServices);

			await context.SaveChangesAsync();
		}

		private static List<Hall> GetHalls() => new() {
			new Hall { Id = HallAId, Name = "Зал А", Capacity = 50, Cost = 2000 },
			new Hall { Id = HallBId, Name = "Зал B", Capacity = 100, Cost = 3500 },
			new Hall { Id = HallCId, Name = "Зал C", Capacity = 30, Cost = 1500 }
		};

		private static List<Service> GetServices() => new() {
			new Service { Id = ProjId, Name = "Проєктор" },
			new Service { Id = WifiId, Name = "Wi-Fi"  },
			new Service { Id = SoundId, Name = "Звук" }
		};

		private static List<HallService> GetHallServices(List<Hall> halls, List<Service> services)
		{
			var list = new List<HallService>();
			foreach (var hall in halls)
			{
				foreach (var service in services)
				{
					list.Add(new HallService { HallId = hall.Id, ServiceId = service.Id, Price = 500 });
				}
			}
			return list;
		}
	}
}
