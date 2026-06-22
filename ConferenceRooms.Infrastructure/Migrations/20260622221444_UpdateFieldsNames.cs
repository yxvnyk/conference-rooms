using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceRooms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_services_bookings_BookingId",
                table: "booking_services");

            migrationBuilder.DropForeignKey(
                name: "FK_booking_services_services_ServiceId",
                table: "booking_services");

            migrationBuilder.DropForeignKey(
                name: "FK_hall_services_halls_HallId",
                table: "hall_services");

            migrationBuilder.DropForeignKey(
                name: "FK_hall_services_services_ServiceId",
                table: "hall_services");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "services",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "halls",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "hall_services",
                newName: "service_id");

            migrationBuilder.RenameColumn(
                name: "HallId",
                table: "hall_services",
                newName: "hall_id");

            migrationBuilder.RenameIndex(
                name: "IX_hall_services_ServiceId",
                table: "hall_services",
                newName: "IX_hall_services_service_id");

            migrationBuilder.RenameIndex(
                name: "IX_hall_services_HallId",
                table: "hall_services",
                newName: "IX_hall_services_hall_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "bookings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "booking_services",
                newName: "service_id");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "booking_services",
                newName: "booking_id");

            migrationBuilder.RenameIndex(
                name: "IX_booking_services_ServiceId",
                table: "booking_services",
                newName: "IX_booking_services_service_id");

            migrationBuilder.RenameIndex(
                name: "IX_booking_services_BookingId_ServiceId",
                table: "booking_services",
                newName: "IX_booking_services_booking_id_service_id");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_services_bookings_booking_id",
                table: "booking_services",
                column: "booking_id",
                principalTable: "bookings",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_booking_services_services_service_id",
                table: "booking_services",
                column: "service_id",
                principalTable: "services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_hall_services_halls_hall_id",
                table: "hall_services",
                column: "hall_id",
                principalTable: "halls",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_hall_services_services_service_id",
                table: "hall_services",
                column: "service_id",
                principalTable: "services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_services_bookings_booking_id",
                table: "booking_services");

            migrationBuilder.DropForeignKey(
                name: "FK_booking_services_services_service_id",
                table: "booking_services");

            migrationBuilder.DropForeignKey(
                name: "FK_hall_services_halls_hall_id",
                table: "hall_services");

            migrationBuilder.DropForeignKey(
                name: "FK_hall_services_services_service_id",
                table: "hall_services");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "services",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "halls",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "service_id",
                table: "hall_services",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "hall_id",
                table: "hall_services",
                newName: "HallId");

            migrationBuilder.RenameIndex(
                name: "IX_hall_services_service_id",
                table: "hall_services",
                newName: "IX_hall_services_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_hall_services_hall_id",
                table: "hall_services",
                newName: "IX_hall_services_HallId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "bookings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "service_id",
                table: "booking_services",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "booking_id",
                table: "booking_services",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_booking_services_service_id",
                table: "booking_services",
                newName: "IX_booking_services_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_booking_services_booking_id_service_id",
                table: "booking_services",
                newName: "IX_booking_services_BookingId_ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_services_bookings_BookingId",
                table: "booking_services",
                column: "BookingId",
                principalTable: "bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_booking_services_services_ServiceId",
                table: "booking_services",
                column: "ServiceId",
                principalTable: "services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_hall_services_halls_HallId",
                table: "hall_services",
                column: "HallId",
                principalTable: "halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_hall_services_services_ServiceId",
                table: "hall_services",
                column: "ServiceId",
                principalTable: "services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
