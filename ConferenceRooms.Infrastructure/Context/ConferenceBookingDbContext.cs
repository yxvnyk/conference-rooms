using ConferenceRooms.Application.Interfaces;
using ConferenceRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ConferenceRooms.Infrastracture.Context
{
	public class ConferenceBookingDbContext : DbContext, IConferenceBookingDbContext
	{
		public ConferenceBookingDbContext(DbContextOptions<ConferenceBookingDbContext> options) : base(options) { }

		public DbSet<Hall> Halls { get; set; }

		public DbSet<Booking> Bookings { get; set; }

		public DbSet<Service> Services { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConferenceBookingDbContext).Assembly);
		}
	}
}
