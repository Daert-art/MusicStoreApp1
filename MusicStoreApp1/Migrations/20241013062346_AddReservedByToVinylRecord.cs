using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStoreApp1.Migrations
{
    /// <inheritdoc />
    public partial class AddReservedByToVinylRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservedBy",
                table: "VinylRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "VinylRecords",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservedBy",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedBy",
                table: "VinylRecords");
        }
    }
}
