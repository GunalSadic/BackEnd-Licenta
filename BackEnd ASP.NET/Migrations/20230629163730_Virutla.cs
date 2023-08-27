using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class Virutla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Matches_LosingPlayerId",
                table: "Matches",
                column: "LosingPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinningPlayerId",
                table: "Matches",
                column: "WinningPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_LosingPlayerId",
                table: "Matches",
                column: "LosingPlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_WinningPlayerId",
                table: "Matches",
                column: "WinningPlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_LosingPlayerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_WinningPlayerId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_LosingPlayerId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_WinningPlayerId",
                table: "Matches");
        }
    }
}
