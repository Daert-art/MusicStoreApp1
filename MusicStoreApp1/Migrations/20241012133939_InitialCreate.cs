using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStoreApp1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VinylRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AlbumName = table.Column<string>(type: "TEXT", nullable: false),
                    ArtistName = table.Column<string>(type: "TEXT", nullable: false),
                    PublisherName = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOfTracks = table.Column<int>(type: "INTEGER", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseYear = table.Column<int>(type: "INTEGER", nullable: false),
                    CostPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    SalePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsOnPromotion = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VinylRecords", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "Id", "AlbumName", "ArtistName", "CostPrice", "Genre", "IsOnPromotion", "NumberOfTracks", "PublisherName", "ReleaseYear", "SalePrice" },
                values: new object[] { 1, "Thriller", "Michael Jackson", 10.00m, "Pop", false, 9, "Epic", 1982, 15.00m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VinylRecords");
        }
    }
}
