using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceRooms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "halls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_halls", x => x.Id);
                    table.CheckConstraint("CK_Halls_Capacity", "\"capacity\" >= 0");
                    table.CheckConstraint("CK_Halls_Cost", "\"cost\" >= 0");
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    hall_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.Id);
                    table.CheckConstraint("CK_Bookings_Time", "end_time > start_time");
                    table.CheckConstraint("CK_Bookings_TotalPrice", "total_price >= 0");
                    table.ForeignKey(
                        name: "FK_bookings_halls_hall_id",
                        column: x => x.hall_id,
                        principalTable: "halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "hall_services",
                columns: table => new
                {
                    HallId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hall_services", x => new { x.HallId, x.ServiceId });
                    table.CheckConstraint("CK_Hall_Service_Price", "\"price\" >= 0");
                    table.ForeignKey(
                        name: "FK_hall_services_halls_HallId",
                        column: x => x.HallId,
                        principalTable: "halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_hall_services_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "booking_services",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    price_at_booking = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_services", x => new { x.BookingId, x.ServiceId });
                    table.CheckConstraint("CK_Booking_Services_PriceAtBooking", "price_at_booking >= 0");
                    table.ForeignKey(
                        name: "FK_booking_services_bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_booking_services_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_services_BookingId_ServiceId",
                table: "booking_services",
                columns: new[] { "BookingId", "ServiceId" });

            migrationBuilder.CreateIndex(
                name: "IX_booking_services_ServiceId",
                table: "booking_services",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_hall_id",
                table: "bookings",
                column: "hall_id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_hall_id_start_time_end_time",
                table: "bookings",
                columns: new[] { "hall_id", "start_time", "end_time" });

            migrationBuilder.CreateIndex(
                name: "IX_hall_services_HallId",
                table: "hall_services",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_hall_services_ServiceId",
                table: "hall_services",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_halls_name",
                table: "halls",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_services_name",
                table: "services",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_services");

            migrationBuilder.DropTable(
                name: "hall_services");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "halls");
        }
    }
}
