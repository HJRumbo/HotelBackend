using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGuestDocumentNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelClassId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HotelClassId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HotelClassId",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DocumentNumber",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "DocumentNumber",
                table: "Guests");

            migrationBuilder.AddColumn<int>(
                name: "HotelClassId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelClassId",
                table: "Rooms",
                column: "HotelClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelClassId",
                table: "Rooms",
                column: "HotelClassId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }
    }
}
