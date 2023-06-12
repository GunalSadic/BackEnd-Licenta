using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class AddTheForeignKeyPleasseee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Elo",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Elo",
                table: "Players");
        }
    }
}
