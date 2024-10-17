using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStoreApp1.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAutoPromotionToVinylRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAutoPromotion",
                table: "VinylRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "VinylRecords",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsAutoPromotion",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAutoPromotion",
                table: "VinylRecords");
        }
    }
}
