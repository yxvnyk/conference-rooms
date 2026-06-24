using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceRooms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReturnBackEndTimeFieldInBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_bookings_hall_id_start_time_duration_hours",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "duration_hours",
                table: "bookings");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_bookings_hall_id_start_time_end_time",
                table: "bookings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Bookings_Time",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "end_time",
                table: "bookings");

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
        }
    }
}
