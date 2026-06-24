using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceRooms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IsDeletedFeildInHall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "halls",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "halls");
        }
    }
}
