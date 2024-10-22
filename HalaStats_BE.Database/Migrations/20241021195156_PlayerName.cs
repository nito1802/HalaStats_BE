using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HalaStats_BE.Database.Migrations
{
    /// <inheritdoc />
    public partial class PlayerName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                schema: "HalaStats",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                schema: "HalaStats",
                table: "Players");
        }
    }
}
