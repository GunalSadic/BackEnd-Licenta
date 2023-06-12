using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class Adaugatmatchesavatars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatar_Users_UserId",
                table: "Avatar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avatar",
                table: "Avatar");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "Avatar",
                newName: "Avatars");

            migrationBuilder.RenameIndex(
                name: "IX_Avatar_UserId",
                table: "Avatars",
                newName: "IX_Avatars_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars",
                column: "AvatarId");

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WinningUserId = table.Column<int>(type: "int", nullable: false),
                    LosingUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Players_UserId",
                table: "Avatars",
                column: "UserId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Players_UserId",
                table: "Avatars");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Avatars",
                newName: "Avatar");

            migrationBuilder.RenameIndex(
                name: "IX_Avatars_UserId",
                table: "Avatar",
                newName: "IX_Avatar_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avatar",
                table: "Avatar",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatar_Users_UserId",
                table: "Avatar",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
