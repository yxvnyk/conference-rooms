using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceRooms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameSomeBookingsFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_bookings_hall_id_start_time_end_time",
                table: "bookings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Bookings_Time",
                table: "bookings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Booking_Services_PriceAtBooking",
                table: "booking_services");

            migrationBuilder.DropColumn(
                name: "end_time",
                table: "bookings");

            migrationBuilder.RenameColumn(
                name: "price_at_booking",
                table: "booking_services",
                newName: "locked_price");

            migrationBuilder.AddColumn<int>(
                name: "duration_hours",
                table: "bookings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_bookings_hall_id_start_time_duration_hours",
                table: "bookings",
                columns: new[] { "hall_id", "start_time", "duration_hours" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Booking_Services_LockedPrice",
                table: "booking_services",
                sql: "locked_price >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_bookings_hall_id_start_time_duration_hours",
                table: "bookings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Booking_Services_LockedPrice",
                table: "booking_services");

            migrationBuilder.DropColumn(
                name: "duration_hours",
                table: "bookings");

            migrationBuilder.RenameColumn(
                name: "locked_price",
                table: "booking_services",
                newName: "price_at_booking");

            migrationBuilder.AddColumn<DateTime>(
                name: "end_time",
                table: "bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_bookings_hall_id_start_time_end_time",
                table: "bookings",
                columns: new[] { "hall_id", "start_time", "end_time" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Bookings_Time",
                table: "bookings",
                sql: "end_time > start_time");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Booking_Services_PriceAtBooking",
                table: "booking_services",
                sql: "price_at_booking >= 0");
        }
    }
}
