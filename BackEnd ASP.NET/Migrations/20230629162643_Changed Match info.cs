using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class ChangedMatchinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WinningUserId",
                table: "Matches",
                newName: "WinningPlayerId");

            migrationBuilder.RenameColumn(
                name: "LosingUserId",
                table: "Matches",
                newName: "LosingPlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WinningPlayerId",
                table: "Matches",
                newName: "WinningUserId");

            migrationBuilder.RenameColumn(
                name: "LosingPlayerId",
                table: "Matches",
                newName: "LosingUserId");
        }
    }
}
