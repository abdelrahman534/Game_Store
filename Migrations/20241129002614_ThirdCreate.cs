using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoGame_Genres_GenreId",
                table: "VideoGame");

            migrationBuilder.DropIndex(
                name: "IX_VideoGame_GenreId",
                table: "VideoGame");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "VideoGame");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "VideoGame",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VideoGame_GenreId",
                table: "VideoGame",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGame_Genres_GenreId",
                table: "VideoGame",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
