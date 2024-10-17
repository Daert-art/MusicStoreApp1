using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStoreApp1.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountPercentageToVinylRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "VinylRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "VinylRecords",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiscountPercentage",
                value: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "VinylRecords");
        }
    }
}
