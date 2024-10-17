using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStoreApp1.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityInStockToVinylRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityInStock",
                table: "VinylRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false),
                    QuantityReserved = table.Column<int>(type: "INTEGER", nullable: false),
                    VinylRecordId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_VinylRecords_VinylRecordId",
                        column: x => x.VinylRecordId,
                        principalTable: "VinylRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "VinylRecords",
                keyColumn: "Id",
                keyValue: 1,
                column: "QuantityInStock",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_VinylRecordId",
                table: "Reservation",
                column: "VinylRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropColumn(
                name: "QuantityInStock",
                table: "VinylRecords");
        }
    }
}
